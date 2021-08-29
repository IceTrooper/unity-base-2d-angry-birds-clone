using DG.Tweening;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private IntVariable score;
    [SerializeField] private TMP_Text scoreText;

    private int displayedScore;
    private Tweener scoreTextTweener;

    private void OnEnable()
    {
        score.Changed.Register(ScoreChanged);
    }

    private void OnDisable()
    {
        score.Changed.Unregister(ScoreChanged);
    }

    private void Start()
    {
        scoreText.text = 0.ToString();

        scoreTextTweener = DOTween.To(x => displayedScore = (int)x, displayedScore, score.Value, 2f)
            .SetEase(Ease.OutCubic)
            .SetAutoKill(false)
            .OnUpdate(() =>
            {
                scoreText.text = displayedScore.ToString();
            });
    }

    private void ScoreChanged(int newScore)
    {
        scoreTextTweener.ChangeEndValue((float)score.Value, true).Restart();
    }

    private void OnDestroy()
    {
        scoreTextTweener.Kill();
    }

    private void Reset()
    {
        scoreText = GetComponent<TMP_Text>();
    }
}
