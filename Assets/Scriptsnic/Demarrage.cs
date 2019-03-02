using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Demarrage : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject instruction;

    public void Play()
    {
        StartScreen.SetActive(false);
       
    }
  
    public void Instructions()
    {
        StartScreen.gameObject.SetActive(false);
        instruction.gameObject.SetActive(true);
    }
    public void QuitGameMP()
    {
        Application.Quit();
    }
    public void Retour()
    {
        StartScreen.gameObject.SetActive(true);
        instruction.gameObject.SetActive(false);
    }
}
