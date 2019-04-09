using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{



    //loads a random level based on the level index in the build settings
    public void LoadByIndex()
    {
        int index = UnityEngine.Random.Range(1, 5);
        SceneManager.LoadScene(index);

    }

    //Quits the application
    public void ExitButton()
    {
        Application.Quit();
    }
}
