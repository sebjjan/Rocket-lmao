using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public AnimationCurve curve;

    public float swingSpeed;
    public float lerp;
    public GameObject bat;
    public Transform startPoint;

    private Vector3 oldPos;
    private Vector3 currentPos;
    private Vector3 newPos;
    private Vector3 tempPos;


    public bool isActing;
   public bool readySwing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            lerp = 0;
            isActing = true;
           // bat.transform.position = startPoint.position;
           // Smash();
        }

       
        
        if(lerp < 1 && readySwing == false)
        {
            ReadySwing();
        }


        if (lerp < 1 && readySwing == true)
        {
            Swing();
            
        }

        if(isActing)
        {
  bat.GetComponent<Rigidbody2D>().MovePosition(currentPos);
        }
        else
        {
            
        }
      

        //bat.transform.position = currentPos;
    }


    public void Smash()
    {
        bat.GetComponent<BatStatus>().Smash();
       
    }

    private void ReadySwing()
    {
        oldPos = bat.transform.position;
        newPos = startPoint.position;
        currentPos = Vector3.Lerp(oldPos, newPos, lerp);
        lerp += Time.deltaTime * swingSpeed;

        if(lerp >= 1)
        {
            readySwing = true;
            lerp = 0;
        }
    }

    public void Swing()
    {
       currentPos = currentPos + new Vector3(curve.Evaluate(lerp), curve.Evaluate(lerp), 0);
        lerp += Time.deltaTime * swingSpeed;
        if(lerp >= 1)
        {
            readySwing = false;
            isActing = false;
        }
    }


    public void StartAttack()
    {
        lerp = 0;
        isActing = true;
    }
}
