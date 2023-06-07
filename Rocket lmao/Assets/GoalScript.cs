using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public bool isBlueTeam;
    public ScoreScript score;
  
    
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
            if (isBlueTeam)
            {
                score.ScoreRed();
            }
            else
            {
                score.ScoreBlue();
            }
         Destroy(collision.gameObject);


        }
       

    }
}
