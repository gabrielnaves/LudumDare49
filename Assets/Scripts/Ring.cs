using UnityEngine;

public class Ring : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnDisable()
    {
        animator.SetBool("Collected", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("Collected", true);
    }
}
