/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace NiobiumStudios
{
    /**
     * The UI Logic Representation of the Daily Rewards
     **/
    public class DailyRewardsInterface : MonoBehaviour
    {
        public Canvas canvas;
        public GameObject dailyRewardPrefab;        // Prefab containing each daily reward

        //[Header("Panel Debug")]
        //public bool isDebug;
        //public GameObject panelDebug;
        //public Button buttonAdvanceDay;
        //public Button buttonAdvanceHour;
        //public Button buttonReset;
        //public Button buttonReloadScene;

        //[Header("Panel Reward Message")]
        //public GameObject panelReward;              // Rewards panel
        //public Text textReward;                     // Reward Text to show an explanatory message to the player
        //public Button buttonCloseReward;            // The Button to close the Rewards Panel
        //public Image imageReward;                   // The image of the reward

        [Header("Panel Reward")]
        public Button buttonClaim;                  // Claim Button
        public GameObject[] DailyBtns, ClaimedBtns;
        //public Button buttonClose;                  // Close Button
        //public Button buttonCloseWindow;            // Close Button on the upper right corner
        public Text textTimeDue;                    // Text showing how long until the next claim
        //public GridLayoutGroup dailyRewardsGroup;   // The Grid that contains the rewards
        //public ScrollRect scrollRect;               // The Scroll Rect

        private bool readyToClaim;                  // Update flag
        private List<DailyRewardUI> dailyRewardsUI = new List<DailyRewardUI>();

		private DailyRewards dailyRewards;			// DailyReward Instance      
        public static DailyRewardsInterface instance;
        public GameObject RewardPanel;
        void Awake()
        {
            instance = this;
            canvas.gameObject.SetActive(false);
			dailyRewards = GetComponent<DailyRewards>();

            HideShineBtns();
        }

        void Start()
        {
            InitializeDailyRewardsUI();

           
            buttonClaim.onClick.AddListener(() =>
            {
				dailyRewards.ClaimPrize();
                readyToClaim = true;
                UpdateUI();
            });

          
            UpdateUI();
        }

        void OnEnable()
        {
            dailyRewards.onClaimPrize += OnClaimPrize;
            dailyRewards.onInitialize += OnInitialize;
        }

        void OnDisable()
        {
            if (dailyRewards != null)
            {
                dailyRewards.onClaimPrize -= OnClaimPrize;
                dailyRewards.onInitialize -= OnInitialize;
            }
        }

        // Initializes the UI List based on the rewards size
        private void InitializeDailyRewardsUI()
        {
            for (int i = 0; i < dailyRewards.rewards.Count; i++)
            {
                int day = i + 1;
                var reward = dailyRewards.GetReward(day);

                GameObject dailyRewardGo = GameObject.Instantiate(dailyRewardPrefab) as GameObject;

                DailyRewardUI dailyRewardUI = dailyRewardGo.GetComponent<DailyRewardUI>();
                //dailyRewardUI.transform.SetParent(dailyRewardsGroup.transform);
                dailyRewardGo.transform.localScale = Vector2.one;

                dailyRewardUI.day = day;
                dailyRewardUI.reward = reward;
                dailyRewardUI.Initialize();

                dailyRewardsUI.Add(dailyRewardUI);
            }
        }

        public void HideShineBtns()
        {
            foreach (GameObject obj in ClaimedBtns)
            {
                obj.SetActive(false);
            }
        }

        public void UpdateUI()
        {
            dailyRewards.CheckRewards();

            bool isRewardAvailableNow = false;

            var lastReward = dailyRewards.lastReward;
            var availableReward = dailyRewards.availableReward;

            HideShineBtns();
            if (lastReward >= 7)
                lastReward = 0;

            //int num = lastReward;
            //if (num > 0)
            //    num--;
            ClaimedBtns[lastReward].SetActive(true);
            foreach (var dailyRewardUI in dailyRewardsUI)
            {
                var day = dailyRewardUI.day;

                if (day == availableReward)
                {
                    
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_AVAILABLE;

                    isRewardAvailableNow = true;
                }
                else if (day <= lastReward)
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.CLAIMED;
                }
                else
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_UNAVAILABLE;
                }

                dailyRewardUI.Refresh();
            }

            buttonClaim.gameObject.SetActive(isRewardAvailableNow);
            textTimeDue.gameObject.transform.parent.gameObject.SetActive(!isRewardAvailableNow);
            //buttonClose.gameObject.SetActive(!isRewardAvailableNow);
            if (isRewardAvailableNow)
            {
                //SnapToReward();
                textTimeDue.gameObject.transform.parent.gameObject.SetActive(false);
                buttonClaim.gameObject.SetActive(true);
                //textTimeDue.text = "You can claim your reward!";
            }
            readyToClaim = isRewardAvailableNow;
        }

        // Snap to the next reward
        public void SnapToReward()
        {
            Canvas.ForceUpdateCanvases();

            var lastRewardIdx = dailyRewards.lastReward;

            // Scrolls to the last reward element
            if (dailyRewardsUI.Count - 1 < lastRewardIdx)
                lastRewardIdx++;

			if(lastRewardIdx > dailyRewardsUI.Count - 1)
				lastRewardIdx = dailyRewardsUI.Count - 1;

            //var target = dailyRewardsUI[lastRewardIdx].GetComponent<RectTransform>();

            //var content = scrollRect.content;

            //content.anchoredPosition = (Vector2)scrollRect.transform.InverseTransformPoint(content.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

            //float normalizePosition = (float)target.GetSiblingIndex() / (float)content.transform.childCount;
            //scrollRect.verticalNormalizedPosition = normalizePosition;
        }

        void Update()
        {

            dailyRewards.TickTime();
            // Updates the time due
            CheckTimeDifference();
        }

        private void CheckTimeDifference ()
        {
            if (!readyToClaim)
            {
                TimeSpan difference = dailyRewards.GetTimeDifference();

                // If the counter below 0 it means there is a new reward to claim
                if (difference.TotalSeconds <= 0)
                {
                    readyToClaim = true;
                    UpdateUI();
                    //SnapToReward();
                    return;
                }

                string formattedTs = dailyRewards.GetFormattedTime(difference);

                textTimeDue.text = string.Format("{0}", formattedTs);
            }
        }

        // Delegate
        private void OnClaimPrize(int day)
        {
            MenuManager.instance.BtnClickSound();
            var reward = dailyRewards.GetReward(day);
            var unit = reward.unit;
            var rewardQt = reward.reward;

            if (day == 1)
            {
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 500);
            }
            else if (day == 2)
            {
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 700);
            }
            else if (day == 3)
            {
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 1000);
            }
            else if (day == 4)
            {
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 1500);
            }
            else if (day == 5)
            {
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 2000);
            }
            else if (day == 6)
            {
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 3000);
            }
            else if (day == 7)
            {
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 5000);
            }


            RewardPanel.SetActive(true);
        }

        private void OnInitialize(bool error, string errorMessage)
        {
            if (!error)
            {
                var showWhenNotAvailable = dailyRewards.keepOpen;
                var isRewardAvailable = dailyRewards.availableReward > 0;

                UpdateUI();
                //canvas.gameObject.SetActive(showWhenNotAvailable || (!showWhenNotAvailable && isRewardAvailable));

                //SnapToReward();
                CheckTimeDifference();
            }
        }
    }
}
