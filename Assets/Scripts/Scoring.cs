using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public Text scoreText;
    public int scorePerRing = 10;

    [ViewOnly] public int score;

    public void ResetScore() => score = 0;

    void Start()
    {
        Ring.OnRingCollected += OnRingCollected;
        UpdateScoreText();
    }

    void OnRingCollected()
    {
        score += scorePerRing;
        UpdateScoreText();
    }

    void UpdateScoreText() => scoreText.text = $"score: { score }";

    void OnDestroy()
    {
        Ring.OnRingCollected -= OnRingCollected;
    }
}
