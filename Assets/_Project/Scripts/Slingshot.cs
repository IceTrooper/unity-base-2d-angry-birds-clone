using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ball;
    [SerializeField] private List<Transform> remainingBalls = new List<Transform>();

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float maxSlingshotDistance = 2f;
    [SerializeField] private float hitPower = 2f;
    [SerializeField] private LineRenderer leftLine;
    [SerializeField] private LineRenderer rightLine;

    private void Start()
    {
        SetBall(ball.gameObject);
    }

    private void Update()
    {
        if(ball != null)
        {
            DrawSlingshotLines();

            if(Input.GetMouseButton(0))
            {
                ball.position = (Vector2)centerPoint.position + GetMaxSlingshotToMouseVector();
            }

            if(Input.GetMouseButtonUp(0))
            {
                ReleaseBall();
            }
        }
    }

    public void SetBall(GameObject ball)
    {
        if(ball == null)
        {
            leftLine.enabled = false;
            rightLine.enabled = false;
            this.ball = null;
            return;
        }

        leftLine.enabled = true;
        rightLine.enabled = true;

        ball.transform.SetPositionAndRotation(centerPoint.position, Quaternion.identity);
        this.ball = ball.GetComponent<Rigidbody2D>();
    }

    private void ReleaseBall()
    {
        ball.constraints = RigidbodyConstraints2D.None;
        ball.AddForce(-GetMaxSlingshotToMouseVector() * hitPower, ForceMode2D.Impulse);

        SetBall(null);
    }

    private Vector2 GetMaxSlingshotToMouseVector()
    {
        //Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 toMouseVector = mouseWorldPosition - centerPoint.position;

        //if(toMouseVector.magnitude > maxSlingshotDistance)
        //{
        //    toMouseVector = toMouseVector.normalized * maxSlingshotDistance;
        //}
        //return toMouseVector;
        Vector2 toMouseVector = mainCamera.ScreenToWorldPoint(Input.mousePosition) - centerPoint.position;
        return Vector2.ClampMagnitude(toMouseVector, maxSlingshotDistance);
    }

    private void DrawSlingshotLines()
    {
        leftLine.SetPosition(1, leftLine.transform.InverseTransformPoint(ball.position));
        rightLine.SetPosition(1, rightLine.transform.InverseTransformPoint(ball.position));
    }
}
