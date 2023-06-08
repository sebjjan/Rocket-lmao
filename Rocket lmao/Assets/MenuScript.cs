using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tennis()
    {
        SceneManager.LoadScene(1);
    }

    public void Basket()
    {
        SceneManager.LoadScene(2);
    }

    public void Football()
    {
        SceneManager.LoadScene(3);
    }
}
