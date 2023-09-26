using UnityEngine;
using DG.Tweening;
public class DotweenAnimationSystem : MonoBehaviour
{
    [Header("Loop count '-1' make loop infinite")]
    public Vector3 Movement;
    public Vector3 Rotation_;
    public Vector3 Scale;

    public Ease easePosition = Ease.Linear;
    public bool playOnEnable = true;
    public bool enablePositionAnimation;
    public bool localPosition;
    public float positionSpeed = 1f;
    public int positionLoop;
    public LoopType positionLoopType = LoopType.Yoyo;
    public float positionDelay;
    public bool enableRotationAnimation;
    public Ease easeRotation = Ease.Linear;
    public bool localRotation;
    public int rotationLoop;


    public float rotationSpeed = 1f;
    public LoopType rotationLoopType = LoopType.Yoyo;
    public float rotationDelay;



    public bool enableScaleAnimation;
    public Ease easeScale = Ease.Linear;
    public LoopType scaleLoopType = LoopType.Yoyo;
    public float scaleDelay;
    public float scaleSpeed = 1f;
    public int scaleLoop;
    private void OnEnable()
    {
        if (playOnEnable)
            PlayAnimation();
    }

    private void PlayAnimation()
    {
        if (enablePositionAnimation)
        {
            if (localPosition)
                transform.DOLocalMove(Movement, positionSpeed).SetEase(easePosition).SetLoops(positionLoop, positionLoopType).SetDelay(positionDelay);
            else
                transform.DOMove(Movement, positionSpeed).SetEase(easePosition).SetLoops(positionLoop, positionLoopType).SetDelay(positionDelay);
        }
        if (enableRotationAnimation)
        {
            if (localRotation)
                transform.DOLocalRotate(Rotation_, rotationSpeed).SetEase(easeRotation).SetLoops(rotationLoop, rotationLoopType).SetDelay(rotationDelay);
            else
                transform.DORotate(Rotation_, rotationSpeed).SetEase(easeRotation).SetLoops(rotationLoop, rotationLoopType).SetDelay(rotationDelay);
        }


        if (enableScaleAnimation)
        {

            transform.DOScale(Scale, scaleSpeed).SetEase(easeScale).SetLoops(scaleLoop, rotationLoopType).SetDelay(scaleDelay);

        }
    }

    private void StopAnimation()
    {

        transform.DOPause();

    }
    private void OnDestroy()
    {
        StopAnimation();
    }
    private void OnDisable()
    {
        StopAnimation();
    }
}