using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject instructionsText;
    public PlayerJuggling playerJuggling;
    public RingSpawner ringSpawner;
    public GameObject scoreText;
    public GameObject gameTime;
    public InputProcessor inputProcessor;
    public GameObject gameOverText;

    IEnumerator Start()
    {
        GameTime.OnGameTimeRanOut += OnGameEnded;

        scoreText.SetActive(false);
        gameTime.SetActive(false);
        gameOverText.SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        scoreText.SetActive(true);
        gameTime.SetActive(true);
        instructionsText.SetActive(false);
        playerJuggling.StartJuggling();
        ringSpawner.StartSpawning();
    }

    void OnGameEnded()
    {
        inputProcessor.enabled = false;
        gameOverText.SetActive(true);
        ringSpawner.ClearRings();
    }

    void OnDestroy()
    {
        GameTime.OnGameTimeRanOut -= OnGameEnded;
    }
}
