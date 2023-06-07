using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatStatus : MonoBehaviour
{
    public Collider2D collider;
    public GameObject myOwner;

    public float force;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(),myOwner.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("ball"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Vector3 direction = new Vector3(1, 1, 0);
          //  collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force * direction, ForceMode2D.Impulse);
            Debug.Log("hit");
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject != myOwner)
            {
                collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                Vector3 direction = new Vector3(1, 1, 0);
              //  collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force * direction, ForceMode2D.Impulse);
            }

        }
    }


    public void Smash()
    {

    }
}
