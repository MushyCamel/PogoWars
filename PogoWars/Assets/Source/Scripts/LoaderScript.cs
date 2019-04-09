using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// *UNUSED AND OBSELETE*
public class LoaderScript : MonoBehaviour
{

    public GameObject gameManager;


    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);
    }


}
