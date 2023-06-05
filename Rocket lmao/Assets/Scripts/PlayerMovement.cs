using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 1;
    public float speedModifyer = 1;
    public Rigidbody2D rb;


    void Start()
    {
        
    }

   

    public void PlayerMovementSide()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
           rb.AddForce(Vector2.left.speed
               )



        }



    }






    // Update is called once per frame
    void Update()
    {
        
    }
}
