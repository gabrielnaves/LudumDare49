using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    static public event System.Action OnGameTimeRanOut;

    public Text timeText;
    public float matchDuration;

    [ViewOnly] public float time;


    void OnEnable()
    {
        time = matchDuration;
    }

    void Update()
    {
        time = Mathf.Max(time - Time.deltaTime, 0);
        timeText.text = Mathf.CeilToInt(time).ToString();
        if (time <= 0)
        {
            OnGameTimeRanOut?.Invoke();
            enabled = false;
        }
    }
}
