using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioSource audio;
    public AudioSource soundEffect;
    public bool clipChosen = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        soundEffect = GetComponent<AudioSource>();
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "Menu" && !clipChosen)
        {
            clipChosen = true;
            audio.clip = clip2;
            audio.Play();
        }
    }

    public enum Planet
    {
        Desert1,
        Pyramide,
        Desert2,
        Eau1,
        Eau2,
        Cave,
        Eau3,
        Terre,
        None
    }
}
