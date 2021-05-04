using Lean.Touch;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField] private Ball loadedBall;
    [HideInInspector] public Rigidbody2D loadedBallRb2D;
    [SerializeField] private List<Ball> balls;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float maxSlingshotDistance = 2f;
    [SerializeField] private float hitPower = 2f;
    [SerializeField] private LineRenderer leftLine;
    [SerializeField] private LineRenderer rightLine;

    [SerializeField] private LineRenderer trajectoryLine;
    [SerializeField] private float predictionTime = 2f;
    private List<Vector3> trajectoryPoints = new List<Vector3>();

    public delegate void ShotHandler(Ball shotBall);
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
        ShowTrajectory(-fingerClampedDelta * hitPower);

        if(finger.Up)
        {
            ReleaseBall(-fingerClampedDelta * hitPower);
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
            loadedBallRb2D = null;
            return;
        }

        ball.transform.SetPositionAndRotation(centerPoint.position, Quaternion.identity);
        loadedBall = ball;
        loadedBallRb2D = ball.GetComponent<Rigidbody2D>();

        leftLine.enabled = true;
        rightLine.enabled = true;
        DrawSlingshotLines();
    }

    private void ReleaseBall(Vector3 hitValue)
    {
        HideTrajectory();
        //var loadedBallRb2D = loadedBall.GetComponent<Rigidbody2D>();
        loadedBallRb2D.constraints = RigidbodyConstraints2D.None;
        loadedBallRb2D.AddForce(hitValue, ForceMode2D.Impulse);

        Shot.Invoke(loadedBall);
        loadedBall.Shot();
        SetBall(null);
    }

    private void DrawSlingshotLines()
    {
        leftLine.SetPosition(1, leftLine.transform.InverseTransformPoint(loadedBall.transform.position));
        rightLine.SetPosition(1, rightLine.transform.InverseTransformPoint(loadedBall.transform.position));
    }

    private void ShowTrajectory(Vector2 hitValue)
    {
        Vector2 pos = loadedBallRb2D.position;
        Vector2 vel = hitValue / loadedBallRb2D.mass;
        float drag = loadedBallRb2D.drag;

        trajectoryPoints.Clear();
        trajectoryPoints.Add(pos);

        float predictedTime = 0;
        while(predictedTime < predictionTime)
        {
            vel += Physics2D.gravity * Time.fixedDeltaTime;
            vel *= Mathf.Clamp01(1.0f - (drag * Time.fixedDeltaTime));
            pos += vel * Time.fixedDeltaTime;
            trajectoryPoints.Add(pos);

            predictedTime += Time.fixedDeltaTime;
        }

        trajectoryLine.positionCount = trajectoryPoints.Count;
        trajectoryLine.SetPositions(trajectoryPoints.ToArray());
        trajectoryLine.enabled = true;
    }

    private void HideTrajectory()
    {
        trajectoryLine.enabled = false;
    }
}
