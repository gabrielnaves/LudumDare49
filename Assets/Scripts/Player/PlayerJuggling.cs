using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJuggling : MonoBehaviour
{
    public int ballAmount = 3;
    public float ballThrowSpeed = 4;
    public GameObject ballPrefab;

    List<GameObject> balls = new List<GameObject>();

    public void ClearBalls()
    {
        foreach (var ball in balls)
            SimplePool.Despawn(ball);
        balls.Clear();
    }

    public void StartJuggling() => StartCoroutine(StartJugglingRoutine());

    IEnumerator StartJugglingRoutine()
    {
        var wait = new WaitForSeconds(CalculateBallThrowingDelay());
        int ballsToSpawn = ballAmount;
        while (ballsToSpawn > 0)
        {
            var ball = SimplePool.Spawn(ballPrefab, transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().velocity = CalculateBallSpeed();
            ballsToSpawn--;
            balls.Add(ball);
            yield return wait;
        }
    }

    float CalculateBallThrowingDelay()
    {
        float delay = -ballThrowSpeed / Physics2D.gravity.y;
        if (ballAmount > 1)
            delay /= ballAmount - 1;
        return delay;
    }

    Vector2 CalculateBallSpeed()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePos - (Vector2)transform.position).normalized * ballThrowSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().velocity = CalculateBallSpeed();
    }
}
