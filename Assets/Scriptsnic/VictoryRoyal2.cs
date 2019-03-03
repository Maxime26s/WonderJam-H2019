using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryRoyal2 : MonoBehaviour
{
    public bool victoryRoyal = false;
    public GameObject menuVic;

    void Update()
    {
        if (victoryRoyal)
        {
            menuVic.SetActive(true);
        }
    }

    public void Rejouer()
    {
        SceneManager.LoadScene("Menu");
        menuVic.SetActive(false);
    }
    public void Quitter()
    {
        Application.Quit();
    }
}
