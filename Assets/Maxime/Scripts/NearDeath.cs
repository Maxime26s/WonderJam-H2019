using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NearDeath : MonoBehaviour
{
    public HealthBar healthBar;
    public Image black;
    public Image red;
    public Image black2;
    public bool dead;
    public float time;
    public float transparent=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            transparent += 0.33f * Time.deltaTime;
            black2.color = new Color(0f, 0f, 0f, transparent);
            if (Time.time - time >= 3)
                SceneManager.LoadScene("Mort", LoadSceneMode.Single);
        } else if (healthBar.currentOxygen <= 50)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, 0.01f * (50 - healthBar.currentOxygen));
            red.color = new Color(red.color.r, red.color.g, red.color.b, 0.01f * (50 - healthBar.currentOxygen));
        }
    }
}
