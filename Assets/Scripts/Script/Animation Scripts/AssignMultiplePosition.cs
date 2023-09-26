using UnityEngine;
using DG.Tweening;
public class AssignMultiplePosition : MonoBehaviour
{

    public RectTransform[] ObjToPosition;
    public Vector3[] PositionsStart;
    public Vector3[] PositionsEnd;
    public float speed;
    public bool OnEnableBool;
    public float DelayFactor;
    public float initialDelay;
    public Ease SpeedType = Ease.Linear;

    private void AssignPosition()
    {
        var total = initialDelay;
        for (var i = 0; i < ObjToPosition.Length; i++)
        {
            ObjToPosition[i].transform.DOPause();
            var rt = ObjToPosition[i].GetComponent<RectTransform>();
            if (rt != null)

                rt.anchoredPosition = PositionsStart[i];
            //  ObjToPosition[i].transform.position = PositionsStart[i];
            rt.DOAnchorPos(PositionsEnd[i], speed).SetEase(SpeedType).SetDelay(total);
            //   ObjToPosition[i].transform.DOLocalMove(PositionsEnd[i], speed).SetEase(SpeedType).SetDelay(total);
            total += DelayFactor;
        }
    }


    private void OnEnable()
    {
        if (ObjToPosition == null) return;
        if (OnEnableBool)
            AssignPosition();
    }
    private void OnDisable()
    {
        foreach (var t in ObjToPosition)
        {
            t.transform.DOPause();
        }
    }
    public void CloseEverything()
    {
        var total = initialDelay;
        for (var i = 0; i < ObjToPosition.Length; i++)
        {
            ObjToPosition[i].transform.DOPause();
            var rt = ObjToPosition[i].GetComponent<RectTransform>();
            if (rt != null)


                rt.DOAnchorPos(PositionsStart[i], speed).SetEase(SpeedType).SetDelay(total);

            total += DelayFactor;
        }
    }
}