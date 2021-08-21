using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using DG.Tweening;

public class Scoreable : MonoBehaviour
{
    [SerializeField] private GameObject pointsTextPrefab;
    [SerializeField] private IntEvent pointsScored;

    public void GetPoints(float pointsAmount)
    {
        GetPoints((int)pointsAmount);
    }

    public void GetPoints(int pointsAmount)
    {
        GetPoints(pointsAmount, pointsTextPrefab);
    }

    public void GetPoints(int pointsAmount, GameObject textPrefab)
    {
        if(pointsAmount == 0) return;

        var spawnPosition = transform.position;
        spawnPosition.z = textPrefab.transform.position.z;

        var pointsText = Instantiate(textPrefab, spawnPosition, Quaternion.identity).GetComponent<TMP_Text>();
        pointsText.text = pointsAmount.ToString();
        pointsScored.Raise(pointsAmount);
    }
}
