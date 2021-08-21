using UnityAtoms.BaseAtoms;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private IntVariable score;
    [SerializeField] private IntEvent pointsScored;

    private void OnEnable()
    {
        pointsScored.Register(ReceivePoints);
    }

    private void OnDisable()
    {
        pointsScored.Unregister(ReceivePoints);
    }

    private void ReceivePoints(int pointsAmount)
    {
        score.Value += pointsAmount;
    }
}
