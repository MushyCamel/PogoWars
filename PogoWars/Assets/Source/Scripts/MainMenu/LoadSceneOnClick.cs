using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]
    private GameManager gameManager;

    public void LoadByIndex()
    {
        int index = UnityEngine.Random.Range(1, 5);
        SceneManager.LoadScene(index);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
