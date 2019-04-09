using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Obselete - Unused due to adaptations in the game manager.
public class DestroyGM : MonoBehaviour
{
    //This script is used to destroy the GameManager if it is present in the MainMenu Screen. This is to wipe the score and data associated with it. As it should spawn on game launch.
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null) { 
        Destroy(GameObject.Find("GameManager"));
        }
    }


}
