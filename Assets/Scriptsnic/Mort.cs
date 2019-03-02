using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mort : MonoBehaviour
{

    public GameObject panelMort;
    public  bool isDead = false;
    // Update is called once per frame
    void Update()
    {
        //détecter s'il est mort et appeler méthode mort if(isDead){mort();}
    }

    public void mort()
    {
        panelMort.SetActive(true);
    }
    public void reessayer()
    {
        panelMort.SetActive(false);
        //Mettre code pour revenir planète 1
    }
    public void Menu()
    {
        panelMort.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
    public void Quitter()
    {
        Application.Quit();
    }

}
