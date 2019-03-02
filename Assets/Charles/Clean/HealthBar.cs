using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject player;

    float currentOxygen = 100f;
    float maxOxygen = 100f;
    public float multiplier = 0.2f;

    float currentTime;
    float oldTime;

    bool isDead = false;
    bool collectingOxygen = false;

    public Sprite[] bars;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.deltaTime;
        oldTime = Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime = Time.deltaTime;
        currentOxygen -= Time.deltaTime * multiplier;

        Debug.Log("Current oxygen = " + currentOxygen);
        Debug.Log("Temps : " +Time.deltaTime);

        if (collectingOxygen)
        {
            currentOxygen = 100;
            oldTime = Time.deltaTime;
        }

        if (currentOxygen >= 10 * (100 /bars.Length))
        {
            this.GetComponent<SpriteRenderer>().sprite = bars[0];
        }

        else if (currentOxygen >= 9 * (100 / bars.Length))
        {
            this.GetComponent<SpriteRenderer>().sprite = bars[1];
        }

        else if (currentOxygen >= 8* (100 / bars.Length))
        {
            this.GetComponent<SpriteRenderer>().sprite = bars[2];
        }

        else if (currentOxygen >= 7 * (100 / bars.Length))
        {
            this.GetComponent<SpriteRenderer>().sprite = bars[3];
        }

        else if (currentOxygen >= 6 * (100 / bars.Length))
        {
            this.GetComponent<SpriteRenderer>().sprite = bars[4];
        }

        else if (currentOxygen >= 5 * (100 / bars.Length))
        {
            this.GetComponent<SpriteRenderer>().sprite = bars[5];
        }

        else if (currentOxygen >= 4 * (100 / bars.Length))
        {
            this.GetComponent<SpriteRenderer>().sprite = bars[6];
        }

        else if (currentOxygen >= 3 * (100 / bars.Length))
        {
            this.GetComponent<SpriteRenderer>().sprite = bars[7];
        }

        else if (currentOxygen >= 2 * (100 / bars.Length))
        {
            this.GetComponent<SpriteRenderer>().sprite = bars[8];
        }

        else if (currentOxygen >= 1 * (100 / bars.Length))
        {
            this.GetComponent<SpriteRenderer>().sprite = bars[9];
        }

        else if (currentOxygen <= 0)
        {
            isDead = true;
            this.GetComponent<SpriteRenderer>().sprite = bars[10];

        }

        if (isDead)
        {
            Death();
        }
    
    }

    void Death()
    {
        player.GetComponent<PlayerController2>().enabled = false;
    }
}
