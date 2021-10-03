using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject instructionsText;
    public PlayerJuggling playerJuggling;
    public RingSpawner ringSpawner;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        instructionsText.SetActive(false);
        playerJuggling.StartJuggling();
        ringSpawner.StartSpawning();
    }
}
