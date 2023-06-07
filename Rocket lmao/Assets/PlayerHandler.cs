using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    private PlayerInputManager manager;
    public GameObject bluePlayer;
    public GameObject redPlayer;
    private int amountOfPlayers = 1;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(manager.playerCount)
        {
            case 1:
                manager.playerPrefab = redPlayer;
                break;
                case 2:
                manager.playerPrefab = bluePlayer;
                break;

            case 3:
                manager.playerPrefab = redPlayer;

                break;
        }
    }



}
