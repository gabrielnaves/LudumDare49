using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject instructionsText;
    public PlayerJuggling playerJuggling;
    public RingSpawner ringSpawner;
    public GameObject scoreText;

    IEnumerator Start()
    {
        scoreText.SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        scoreText.SetActive(true);
        instructionsText.SetActive(false);
        playerJuggling.StartJuggling();
        ringSpawner.StartSpawning();
    }
}
