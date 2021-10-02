using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration = 4;
    public float maxSpeed = 4;

    Rigidbody2D body;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [ViewOnly] public float speed;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnDisable()
    {
        speed = 0;
        body.velocity = new Vector2(0, body.velocity.y);
    }

    void Update()
    {
        UpdateVisualsWithMovement();
    }

    private void UpdateVisualsWithMovement()
    {
        animator.SetBool("Moving", InputData.Horizontal != 0);
        if (InputData.Horizontal < 0)
            spriteRenderer.flipX = true;
        if (InputData.Horizontal > 0)
            spriteRenderer.flipX = false;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (InputData.Horizontal != 0)
        {
            if (Mathf.Sign(InputData.Horizontal) != Mathf.Sign(body.velocity.x))
                speed *= 0.5f;
            speed += acceleration * Time.deltaTime;
            speed = Mathf.Min(speed, maxSpeed);
        }
        else
        {
            speed -= acceleration * 2 * Time.deltaTime;
            speed = Mathf.Max(speed, 0);
        }
        body.velocity = new Vector2(InputData.Horizontal * speed, body.velocity.y);
    }
}
