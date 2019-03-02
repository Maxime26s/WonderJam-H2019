using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NearDeath : MonoBehaviour
{
    public HealthBar healthBar;
    public Image black;
    public Image red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.currentOxygen <= 50)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, 0.01f * (50 - healthBar.currentOxygen));
            red.color = new Color(red.color.r, red.color.g, red.color.b, 0.01f * (50 - healthBar.currentOxygen));
        }
    }
}
