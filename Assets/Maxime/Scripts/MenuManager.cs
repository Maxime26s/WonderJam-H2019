using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject explication;
    public GameObject menu;
    public GameObject title;

    public void changeSceneToGame()
    {
        SceneManager.LoadScene("Lune");
    }

    public void exit()
    {
        Application.Quit();
    }
}
