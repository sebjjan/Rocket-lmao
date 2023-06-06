using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isJumping = false;
    public float maxSpeed;

    private Vector2 horizontalInput;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");

        // Move horizontally
      //  Vector2 movement = new Vector2(speed, rb.velocity.y) * horizontalInput;
       // rb.velocity = movement;

        // Jump
      /*  if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
      */
    }
    private void FixedUpdate()
    {
        
        rb.velocity = new Vector2(Vector2.ClampMagnitude(speed * horizontalInput, maxSpeed).x, rb.velocity.y);
    }
    public void Move(CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>();
       // rb.velocity += /* new Vector2(speed, rb.velocity.y)*/ speed * horizontalInput;
      //  rb.AddForce(speed * horizontalInput);
    }

    public void Jump()
    {
        Debug.Log("i want to jump");
        if (!isJumping)
        {
            Debug.Log("jump");
 rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isJumping = true;
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if grounded
        if (collision.gameObject.CompareTag("ground"))
        {
            isJumping = false;
        }
    }
}
