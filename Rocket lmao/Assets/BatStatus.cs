using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatStatus : MonoBehaviour
{
    public Collider2D collider;

    public float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ball"))
        {
            Vector3 direction = collision.transform.forward;
            gameObject.GetComponent<Rigidbody2D>().AddForce(force * direction);
        }
    }
}
