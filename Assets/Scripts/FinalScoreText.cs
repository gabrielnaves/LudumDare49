using UnityEngine;
using UnityEngine.UI;

public class FinalScoreText : MonoBehaviour
{
    public Scoring scoring;
    public Text text;

    void OnEnable()
    {
        text.text = $"final score: {scoring.score}";
    }
}
