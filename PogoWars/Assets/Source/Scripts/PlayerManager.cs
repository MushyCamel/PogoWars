using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerManager
{
    public Color playerColour;
    public GameObject playerPrefab;
    [HideInInspector] public int playerNumber;            
    [HideInInspector] public string colouredPlayerText;    
    [HideInInspector] public GameObject instance;         
    [HideInInspector] public int wins;                    

    private PlayerController _movement;                       
    private GameObject _canvasGameObject; 


    public void Setup()
    {
        
        //Assigns the players components based on the player number, and assigns them a text colour.
        _movement = instance.GetComponent<PlayerController>();
        _canvasGameObject = instance.GetComponentInChildren<Canvas>().gameObject;

        _movement.playerNumber = playerNumber;

        colouredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColour) + ">PLAYER " + playerNumber + "</color>";
    }


    // Used at the start of each round to put the player into it's default state.
    public void Reset(Transform spawnPoint)
    {
        instance.transform.position = spawnPoint.transform.position;
        instance.transform.rotation = spawnPoint.transform.rotation;

        instance.SetActive(false);
        instance.SetActive(true);

    }
}
