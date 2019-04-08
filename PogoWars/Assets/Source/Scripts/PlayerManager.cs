using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerManager
{
    public Color _playerColour;
    public GameObject _playerPrefab;
    [HideInInspector] public int _playerNumber;            
    [HideInInspector] public string _colouredPlayerText;    
    [HideInInspector] public GameObject _instance;         
    [HideInInspector] public int _wins;                    

    private PlayerController _movement;                        // Reference to tank's movement script, used to disable and enable control.
    private GameObject _canvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.

    // Start is called before the first frame update
    public void Setup()
    {
        

        _movement = _instance.GetComponent<PlayerController>();
        _canvasGameObject = _instance.GetComponentInChildren<Canvas>().gameObject;

        _movement._playerNumber = _playerNumber;

        _colouredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(_playerColour) + ">PLAYER " + _playerNumber + "</color>";
    }


    // Used at the start of each round to put the tank into it's default state.
    public void Reset(Transform spawnPoint)
    {
        _instance.transform.position = spawnPoint.transform.position;
        _instance.transform.rotation = spawnPoint.transform.rotation;

        _instance.SetActive(false);
        _instance.SetActive(true);

    }
}
