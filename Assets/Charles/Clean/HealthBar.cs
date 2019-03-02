using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public Scene sceneMort;
    public Image noir;


    public float currentOxygen = 100f;
    public float maxOxygen = 100f;
    public float multiplier = 0.2f;

    float currentTime;
    float oldTime;

    public bool isDead = false;
    public bool collectingOxygen = false;

    public Sprite[] bars;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.deltaTime;
        oldTime = Time.deltaTime;
        noir.color = new Color(1, 1, 1, 0);
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
            collectingOxygen = false;
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
        player.GetComponent<GameManager>().enabled = false;
        
        StartCoroutine(FadeImage(true));

        SceneManager.LoadScene("Mort", LoadSceneMode.Single);

    }

       

        IEnumerator FadeImage(bool fadeAway)
        {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            noir.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
    }
