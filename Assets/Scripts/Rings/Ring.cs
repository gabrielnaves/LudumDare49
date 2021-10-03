using UnityEngine;

public class Ring : MonoBehaviour
{
    static public event System.Action OnRingCollected;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnDisable()
    {
        animator.SetBool("Collected", false);
    }

    void OnTriggerEnter2D(Collider2D _)
    {
        animator.SetBool("Collected", true);
        OnRingCollected?.Invoke();
    }
}
