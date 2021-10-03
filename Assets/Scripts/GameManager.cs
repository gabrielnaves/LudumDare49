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

    void Start()
    {
        GameTime.OnGameTimeRanOut += OnGameEnded;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        instructionsText.SetActive(true);
        scoreText.SetActive(false);
        gameTime.SetActive(false);
        gameOverText.SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        scoreText.SetActive(true);
        gameTime.SetActive(true);
        gameTime.GetComponentInChildren<GameTime>().enabled = true;
        instructionsText.SetActive(false);
        playerJuggling.StartJuggling();
        ringSpawner.StartSpawning();
    }

    void OnGameEnded()
    {
        gameOverText.SetActive(true);
        ringSpawner.ClearRings();
        gameTime.SetActive(false);
        playerJuggling.ClearBalls();
        StartCoroutine(WaitForGameToRestart());
    }

    IEnumerator WaitForGameToRestart()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return null;
        StartCoroutine(StartGame());
    }

    void OnDestroy()
    {
        GameTime.OnGameTimeRanOut -= OnGameEnded;
    }
}
