using UnityEngine;

public class BallsActivator : MonoBehaviour
{
    [SerializeField] private Slingshot slingshot;

    private Ball activeBall;

    private void OnEnable()
    {
        slingshot.Shot += OnShot;
    }

    private void OnDisable()
    {
        slingshot.Shot -= OnShot;
    }

    private void OnShot(Ball shotBall)
    {
        activeBall = shotBall;
    }

    public void ActivateBall()
    {
        if(activeBall != null)
        {
            activeBall.Activate();
        }
    }
}
