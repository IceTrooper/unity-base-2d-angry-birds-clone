using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    [SerializeField] private float inDuration = 0.5f;
    [SerializeField] private float outDuration = 0.5f;
    [SerializeField] private float intervalDuration = 0f;
    [SerializeField] private float endingScale = 1.5f;
    [SerializeField] private bool doPunchRotation = true;
    [SerializeField, ShowIf("doPunchRotation")] private float punchPower = 60f;

    private Sequence showSequence;

    private void Start()
    {
        var rectTransform = GetComponent<RectTransform>();
        showSequence = DOTween.Sequence()
            .Append(rectTransform.DOScale(endingScale, inDuration).From(0f)).SetEase(Ease.OutCubic);
        if(doPunchRotation)
        {
            showSequence.Join(rectTransform.DOPunchRotation(Vector3.forward * punchPower, inDuration));
        }
        showSequence.AppendInterval(intervalDuration)
            .Append(rectTransform.DOScale(0f, outDuration)).SetEase(Ease.InCubic)
            .OnComplete(() => Destroy(gameObject))
            .OnKill(() => showSequence = null)
            .Play();
    }

    private void OnDestroy()
    {
        if(showSequence != null) showSequence.Kill();
    }
}
