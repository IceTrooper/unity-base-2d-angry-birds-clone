using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField] private Ball loadedBall;
    [SerializeField] private List<Ball> balls;
    //[SerializeField] private List<Transform> remainingBalls = new List<Transform>();

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float maxSlingshotDistance = 2f;
    [SerializeField] private float hitPower = 2f;
    [SerializeField] private LineRenderer leftLine;
    [SerializeField] private LineRenderer rightLine;

    public delegate void ShotHandler();
    public event ShotHandler Shot;

    private void Start()
    {
        //SetBall(loadedBall);
    }

    private void Update()
    {
        if(loadedBall != null)
        {
            DrawSlingshotLines();

            if(Input.GetMouseButton(0))
            {
                loadedBall.transform.position = (Vector2)centerPoint.position + GetMaxSlingshotToMouseVector();
            }

            if(Input.GetMouseButtonUp(0))
            {
                ReleaseBall();
            }
        }
    }

    public Ball Reload()
    {
        if(balls.Count == 0) return null;

        for(int i = balls.Count - 1; i > 0; i--)
        {
            Vector3 newPosition = balls[i - 1].transform.position;
            newPosition.y = balls[i].transform.position.y;
            balls[i].transform.position = newPosition;
        }

        SetBall(balls[0]);
        balls.RemoveAt(0);

        return loadedBall;
    }

    private void SetBall(Ball ball)
    {
        if(ball == null)
        {
            leftLine.enabled = false;
            rightLine.enabled = false;
            loadedBall = null;
            return;
        }

        leftLine.enabled = true;
        rightLine.enabled = true;

        ball.transform.SetPositionAndRotation(centerPoint.position, Quaternion.identity);
        loadedBall = ball;
    }

    private void ReleaseBall()
    {
        var loadedBallRb2D = loadedBall.GetComponent<Rigidbody2D>();
        loadedBallRb2D.constraints = RigidbodyConstraints2D.None;
        loadedBallRb2D.AddForce(-GetMaxSlingshotToMouseVector() * hitPower, ForceMode2D.Impulse);

        loadedBall.Die();
        SetBall(null);
        Shot.Invoke();
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
        leftLine.SetPosition(1, leftLine.transform.InverseTransformPoint(loadedBall.transform.position));
        rightLine.SetPosition(1, rightLine.transform.InverseTransformPoint(loadedBall.transform.position));
    }
}
