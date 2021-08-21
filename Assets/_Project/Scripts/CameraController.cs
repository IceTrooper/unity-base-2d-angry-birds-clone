using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [System.Serializable]
    public struct CameraPoint
    {
        public Vector3 position;
        public float zoom;
    }

    [SerializeField] private CameraPoint startPoint;
    [SerializeField] private CameraPoint endPoint;
    [SerializeField] private float speed = 5f;

    private Camera mainCamera = null;

    private Sequence startSequence;
    private Sequence focusLevelSequence;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        float movingDuration = Vector3.Distance(transform.position, startPoint.position) / speed;

        startSequence = DOTween.Sequence()
            .AppendInterval(0.5f)
            .Append(mainCamera.DOOrthoSize(endPoint.zoom, 1.5f))
            .Append(transform.DOMove(startPoint.position, movingDuration))
            .Join(mainCamera.DOOrthoSize(startPoint.zoom, movingDuration))
            .OnComplete(() => startSequence = null)
            .Play();
    }

    public void FocusLevel()
    {
        float focusDuration = Vector3.Distance(transform.position, endPoint.position) / speed;
        float backDuration = Vector3.Distance(startPoint.position, endPoint.position) / speed;

        focusLevelSequence = DOTween.Sequence()
            .Append(transform.DOMove(endPoint.position, focusDuration))
            .Join(mainCamera.DOOrthoSize(endPoint.zoom, focusDuration))
            .AppendInterval(4f)
            .Append(transform.DOMove(startPoint.position, backDuration))
            .Join(mainCamera.DOOrthoSize(startPoint.zoom, backDuration))
            .OnComplete(() => focusLevelSequence = null)
            .Play();
    }

    public void MoveHorizontal(Vector3 delta)
    {
        var clampedX = Mathf.Clamp(transform.position.x - delta.x, startPoint.position.x, endPoint.position.x);
        var newPosition = transform.position;
        newPosition.x = clampedX;
        transform.position = newPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPoint.position, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endPoint.position, 0.5f);
    }

    private void OnDestroy()
    {
        if(startSequence != null) startSequence.Kill();
        if(focusLevelSequence != null) focusLevelSequence.Kill();
    }
}
