using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isJumping = false;
    public float maxSpeed;

    public List<GameObject> flipList;

    public Vector2 horizontalInput;

    public GameObject batHand;

    private Vector2 mouseWorldPos;

    private PlayerInput playerInput;

    private Vector2 stickAim;
    public float crosshairDistance;

    public GameObject bat;

    public float armForce;
    public float controllerarmForce;

    public GameObject crossHair;



    private void Awake()
    {

    }
    private void Start()
    {
        // rb.MovePosition(new Vector3(0, 100, 0));
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

    }

    private void Update()
    {

        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mouseWorldPos.z = 0;

        if (playerInput.currentControlScheme == "Gamepad")
        {
            // Crosshair.transform.position = (Vector2)transform.position + stickAim * crosshairDistance;
        }
        else
        {
            // Crosshair.transform.position = mouseWorldPos;
        }

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

        if (playerInput.currentControlScheme == "Gamepad")
        {
            crossHair.transform.position = (Vector2)transform.position + stickAim * crosshairDistance;

            // Vector2 test = stickAim * armForce;
            // batHand.transform.right = stickAim;
            batHand.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            batHand.GetComponent<Rigidbody2D>().AddForce((crossHair.transform.position - batHand.transform.position).normalized * controllerarmForce);

            //bat.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            bat.GetComponent<Rigidbody2D>().AddForce((crossHair.transform.position - bat.transform.position).normalized * controllerarmForce);
            // batHand.GetComponent<Rigidbody2D>().AddForce((mouseWorldPos - (Vector2)batHand.transform.position).normalized * armForce);


        }
        else
        {
            //
            //  batHand.GetComponent<Rigidbody2D>().AddTorque(1000);
            /*  Quaternion rotation = Quaternion.LookRotation(mouseWorldPos - (Vector2)batHand.transform.position);
              print(rotation);
              batHand.GetComponent<Rigidbody2D>().MoveRotation(rotation);

              */
            //batHand.GetComponent<Rigidbody2D>().MoveRotation(Quaternion.LookRotation((mouseWorldPos - (Vector2)batHand.transform.position).normalized), );
            //batHand.transform.right = (mouseWorldPos - (Vector2)batHand.transform.position).normalized;
            batHand.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            batHand.GetComponent<Rigidbody2D>().AddForce((mouseWorldPos - (Vector2)batHand.transform.position).normalized * armForce);

            //  bat.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            bat.GetComponent<Rigidbody2D>().AddForce((mouseWorldPos - (Vector2)bat.transform.position).normalized * armForce);
        }


        // rb.velocity = new Vector2(Vector2.ClampMagnitude(speed * horizontalInput, maxSpeed).x, rb.velocity.y);

        /*
        if (true )
        {
            rb.AddForce(new Vector2(speed*horizontalInput.x, 0));
        }
        */


        if (rb.velocity.x < maxSpeed && rb.velocity.x > -maxSpeed)
        {
            rb.AddForce(new Vector2(speed * horizontalInput.x, 0));
        }






    }
    public void Move(CallbackContext context)
    {


        if (horizontalInput != context.ReadValue<Vector2>())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        horizontalInput = context.ReadValue<Vector2>();

        if (horizontalInput.x < 0)
        {
            foreach (GameObject g in flipList)
            {
                g.GetComponent<Transform>().localPosition = new Vector3(-40, -20, 1);
            }
            //GetComponent<Transform>().localScale = new Vector3(-1,1,1);
            // IKStuff.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            foreach (GameObject g in flipList)
            {
                g.GetComponent<Transform>().localPosition = new Vector3(40, -20, 1);
                // gameObject.GetComponent<Transform>().localPosition += new Vector3(40, 0, 0);
            }
            //GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            //IKStuff.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        // rb.velocity += /* new Vector2(speed, rb.velocity.y)*/ speed * horizontalInput;
        //  rb.AddForce(speed * horizontalInput);
    }




    public void Jump()
    {

        if (!isJumping)
        {

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



    public void Aim(CallbackContext context)
    {
        if (context.ReadValue<Vector2>() != Vector2.zero)
        {
            stickAim = context.ReadValue<Vector2>();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, mouseWorldPos - (Vector2)batHand.transform.position);

    }


    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void WalkRight()
    {
        rb.AddForce(new Vector2(speed, 0));

        foreach (GameObject g in flipList)
        {
            g.GetComponent<Transform>().localPosition = new Vector3(40, -20, 1);
        }
    }

    public void WalkLeft()
    {


         rb.AddForce(new Vector2(-speed, 0));

        foreach (GameObject g in flipList)
        {
            g.GetComponent<Transform>().localPosition = new Vector3(-40, -20, 1);
        }
        //GetComponent<Transform>().localScale = new Vector3(-1,1,1);
        // IKStuff.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
    }


}


