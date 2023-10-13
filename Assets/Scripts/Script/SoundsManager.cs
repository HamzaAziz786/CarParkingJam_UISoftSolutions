using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;
    public AudioClip levelWinSound;
    public AudioClip levelFailSound;
    public AudioClip[] coinCollectSounds;
    public AudioClip[] bonusCollectSounds;
    public AudioClip buttonClipSound;
    public AudioClip gemCollectSound;
    public AudioClip[] humanKillSounds;
    public AudioClip selectionSound;
    public AudioClip hitSound;
    public AudioClip backGroundSound;
    public AudioClip achivementSound;
    public AudioSource AS;
    public AudioSource bg;
    public AudioClip Road, Mud;
    public AudioClip confeetti;
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PlayBackGroundSound(bg);
    }
    public void PlayClipH()
    {
        audioSource.PlayOneShot(buttonClipSound);
    }
    private static void PlaySound(AudioClip a, AudioSource @as)
    {

        //if (a == null)
        //    return;
        //if (@as == null)
        //    return;
        //if (@as.mute)
        //    return;

        @as.clip = a;
        @as.loop = false;
        @as.Play();
    }
    public void PlayLevelWinSound(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(levelWinSound, @as);
    }
    public void StopMusic()
    {
        bg.enabled = false;
    }
    public void StartMusic()
    {
        bg.enabled = true;
    }
    public void PlayLevelFailSound(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(levelFailSound, @as);
    }
    public void PlayCoinCollectSound(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(coinCollectSounds[Random.Range(0, coinCollectSounds.Length)], @as);
    }
    public void PlayBonusCollectSound(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(bonusCollectSounds[Random.Range(0, bonusCollectSounds.Length)], @as);
    }

    public void PlayButtonClipSound(AudioSource @as)
    {
        ////if (@as == null)
        ////    return;
        @as.volume = 1f;
        PlaySound(buttonClipSound, @as);
    }

    public void PlayGemCollectSound(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(gemCollectSound, @as);
    }
    public void PlayHumanKillSounds(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(humanKillSounds[Random.Range(0, humanKillSounds.Length)], @as);
    }

    public void PlaySelectionSound(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(selectionSound, @as);
    }

    public void PlayHitSound(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(hitSound, @as);
    }
    public void PlayBackGroundSound(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(backGroundSound, @as);
    }
    public void PlayAchievementSound(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(achivementSound, @as);
    }

    public void RoadFlip(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(Road, @as);
    }
    
    public void MudFlip(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(Mud, @as);
    }
    public void WinConfetti(AudioSource @as)
    {
        if (@as == null)
            return;
        @as.volume = 1f;
        PlaySound(confeetti, @as);
    }
}
