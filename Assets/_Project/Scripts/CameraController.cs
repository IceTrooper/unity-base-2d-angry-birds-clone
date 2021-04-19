using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float speed = 2f;

    private Camera mainCamera = null;

    private Vector3 targetPosition;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        MoveStart();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void MoveStart()
    {
        targetPosition = startPosition;
    }

    public void MoveEnd()
    {
        targetPosition = endPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPosition, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endPosition, 0.5f);
    }
}
