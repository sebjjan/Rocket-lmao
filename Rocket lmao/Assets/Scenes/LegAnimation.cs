using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LegAnimation : MonoBehaviour
{
    public GameObject target1;
    public GameObject oppositeLeg;
    public float lerpSpeed;
    public float maxDistance;
    public GameObject balance;
    public Collider2D searchRange;
    public ContactFilter2D contactFilter;

    Vector3 footTarget;

   public float lerp = 0;
    private Vector3 oldPos;
    private Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        FindTarget();




        if (Vector3.Distance(target1.transform.position, balance.transform.position) > maxDistance && lerp >= 1)
        {
            lerp = 0;

            Debug.Log("out of balance");

           

        }

        MoveFoot();
    }


    private void MoveFoot()
    {
        if(lerp < 1)
        {
            //Debug.Log("move");
            oldPos = target1.transform.position;
            newPos = footTarget;

            target1.transform.position = Vector3.Lerp(oldPos, newPos, lerp);
            lerp += Time.deltaTime * lerpSpeed;
        }
      

        //fixa den
        //target1.transform.position.y = Mathf.Sin(lerp);

    }

    private void FindTarget()
    {
        Collider2D closestCollider = null;
        List<Collider2D> colliders = new List<Collider2D>();
        searchRange.OverlapCollider(contactFilter, colliders);
        foreach(Collider2D c in colliders)
        {
            if(closestCollider == null)
            {
                closestCollider = c;
            }
            else if(Vector3.Distance(balance.transform.position, c.ClosestPoint(balance.transform.position)) < Vector3.Distance(balance.transform.position, closestCollider.ClosestPoint(balance.transform.position)))
                    {
                closestCollider = c;
            }
        }
        if(closestCollider != null)
        {
    footTarget = closestCollider.ClosestPoint(balance.transform.position);
            newPos = footTarget;
        }
    
       // Vector3 target = Physics2D.Linecast()
    }



    private void OnDrawGizmos()
    {
        /*
        Gizmos.DrawSphere(footTarget, 2);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(oldPos, 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(newPos, 1);
        Gizmos.DrawWireSphere(balance.transform.position, 5);
        */
    }

}
