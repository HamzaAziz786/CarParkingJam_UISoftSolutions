using UnityEngine;
using DG.Tweening;

public class AssignMultipleScaleOnStart : MonoBehaviour
{

    public Vector3 ScaleStart = Vector3.zero;
    public Vector3 ScaleEnd = Vector3.one;
    public Transform[] ObjToScale;
    public float speed;
    public bool OnEnableBool;
    public float DelayFactor;
    public float initialDelay;
    public Ease SpeedType = Ease.Linear;

    private void AssignScales()
    {
        var total = initialDelay;
        foreach (var t in ObjToScale)
        {
            Transform transform1;
            (transform1 = t.transform).DOPause();
            transform1.localScale = ScaleStart;
            t.transform.DOScale(ScaleEnd, speed).SetEase(SpeedType).SetDelay(total);
            total += DelayFactor;
        }
    }
    private void OnEnable()
    {
        if (ObjToScale == null) return;
        if (OnEnableBool)
            AssignScales();
    }
    private void OnDisable()
    {
        foreach (var t in ObjToScale)
        {
            t.transform.DOPause();
        }
    }
}