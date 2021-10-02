using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;

    Rigidbody2D body;
    Animator animator;
    SpriteRenderer spriteRenderer;

    float horizontal;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontal = InputData.Horizontal;
        animator.SetBool("Moving", horizontal != 0);
        if (horizontal < 0)
            spriteRenderer.flipX = true;
        if (horizontal > 0)
            spriteRenderer.flipX = false;
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * movementSpeed, body.velocity.y);
    }
}
