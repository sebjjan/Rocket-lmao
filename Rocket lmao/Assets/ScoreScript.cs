using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    public TMP_Text blueText;
    public TMP_Text redText;



    private int totalScore;
    private int blueScore = 0;
    private int redScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ScoreBlue()
    {
        blueScore++;
        UpdateScore();
    }


    public void ScoreRed()
    {
        redScore++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        blueText.text = blueScore.ToString();
        redText.text = redScore.ToString();


        Instantiate(ballPrefab, ballSpawnPoint);

        if (totalScore > 3)
        {
            Instantiate(ballPrefab, ballSpawnPoint);
        }

    }


}
