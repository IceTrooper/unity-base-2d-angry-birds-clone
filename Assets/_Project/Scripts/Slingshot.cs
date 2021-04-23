using Lean.Touch;
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

    public void HandleTouchUpdate(LeanFinger finger)
    {
        if(loadedBall == null) return;

        DrawSlingshotLines();

        var fingerWorldDelta = finger.GetLastWorldPosition(0) - finger.GetStartWorldPosition(0);
        var fingerClampedDelta = Vector2.ClampMagnitude(fingerWorldDelta, maxSlingshotDistance);

        loadedBall.transform.position = (Vector2)centerPoint.position + fingerClampedDelta;

        if(finger.Up)
        {
            ReleaseBall(-fingerClampedDelta);
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

        ball.transform.SetPositionAndRotation(centerPoint.position, Quaternion.identity);
        loadedBall = ball;

        leftLine.enabled = true;
        rightLine.enabled = true;
        DrawSlingshotLines();
    }

    private void ReleaseBall(Vector3 direction)
    {
        var loadedBallRb2D = loadedBall.GetComponent<Rigidbody2D>();
        loadedBallRb2D.constraints = RigidbodyConstraints2D.None;
        loadedBallRb2D.AddForce(direction * hitPower, ForceMode2D.Impulse);

        loadedBall.Die();
        SetBall(null);
        Shot.Invoke();
    }

    private void DrawSlingshotLines()
    {
        leftLine.SetPosition(1, leftLine.transform.InverseTransformPoint(loadedBall.transform.position));
        rightLine.SetPosition(1, rightLine.transform.InverseTransformPoint(loadedBall.transform.position));
    }
}
