using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public AnimationCurve curve;

    public float swingSpeed;
    public float lerp;
    public GameObject bat;
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
            Smash();
        }

        bat.transform.position = new Vector3(curve.Evaluate(lerp), curve.Evaluate(lerp), 0) * 3;

        if (lerp < 1)
        {
            lerp += Time.deltaTime * swingSpeed;
        }
    }


    public void Smash()
    {

       
    }
}
