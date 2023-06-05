using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LegAnimation : MonoBehaviour
{
    public GameObject target1;
    public LegAnimation oppositeLeg;
    public float lerpSpeed;
    public float maxDistance;
    public GameObject balance;
    public float stepHeight;
  
    public ContactFilter2D contactFilter;
    public Transform RayCaster;
    public LayerMask layerMask;

    private Vector3 tempPos;
   // public GameObject Test;

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




        if (Vector3.Distance(target1.transform.position, balance.transform.position) > maxDistance && lerp >= 1 && oppositeLeg.lerp >= 1)
        {
           
                lerp = 0;
            
            

            Debug.Log("out of balance");

           

        }
        if(lerp == 0)
        {
            oldPos = target1.transform.position;
            tempPos = footTarget;
            MoveFoot();
        }
        if(lerp < 1)
        {
            MoveFoot();
        }

        target1.transform.position = newPos;
      
    }


    private void MoveFoot()
    {
        
            //Debug.Log("move");
          
        //Vector3 tempTarget = footTarget;
           // newPos = footTarget;

            newPos = Vector3.Lerp(oldPos, tempPos, lerp);
        newPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
        //newPos += new Vector3(0, Mathf.Sin(lerp) * 10,0);
        lerp += Time.deltaTime * lerpSpeed;
       
      

        //fixa den
        //target1.transform.position.y = Mathf.Sin(lerp);

    }

    private void FindTarget()
    {
        /*
        Collider2D closestCollider = null;
        List<Collider2D> colliders = new List<Collider2D>();
        searchRange.OverlapCollider(contactFilter, colliders);
        */
        RaycastHit2D rayHit = Physics2D.Raycast(RayCaster.position, Vector2.down, 100000, layerMask);
        if(rayHit.point != Vector2.zero)
        {
           
            footTarget = rayHit.point;
           // Test.transform.position = rayHit.point;
            if (rayHit.point == (Vector2)newPos)
            {
                Debug.Log("YIPPU");
            }
           // Debug.Log(rayHit.point);

        }



        /*
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
        */

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

        Gizmos.DrawWireSphere(newPos, 4);
        Gizmos.DrawLine(RayCaster.position, newPos);
        Gizmos.DrawWireSphere(balance.transform.position, maxDistance);
       // Gizmos.DrawCube(testPosition.position, new Vector3(2,2,2));
    }

}
