using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlatController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Planet nextPlanet;
    public Animator animator;
    public bool action, actionDone, end;
    public float timerAction, cdAction, time, transparent;
    public GameObject[] inventory = new GameObject[5];
    public GameObject interactable, oxygenBar;
    public Image black2;

    void Update()
    {
        if (end)
        {
            transparent += 0.33f * Time.deltaTime;
            black2.color = new Color(0f, 0f, 0f, transparent);
            if (Time.time - time >= 3)
                changeScene();
        }
        else
        {
            if (Input.GetKey(KeyCode.E)  && interactable != null)
                    {
                        oxygenBar.GetComponent<HealthBar>().collectingOxygen = true;
                        timerAction = Time.time;
                        action = true;
                        playerMovement.action = true;
                        switch (interactable.tag)
                        {
                            case "air":
                                animator.SetBool("action", true);
                                use(Type.air, interactable);
                                break;
                            case "chest":
                                animator.SetBool("action", true);
                                use(Type.chest, interactable);
                                break;
                            default:
                                action = false;
                                playerMovement.action = false;
                                break;
                        }
                    }
                    if (Input.GetKey(KeyCode.E))
                    {
                        actionDone = true;
                    }
                    if (Time.time - timerAction > cdAction && actionDone)
                    {
                        action = false;
                        playerMovement.action = false;
                        oxygenBar.GetComponent<HealthBar>().collectingOxygen = false;
                        animator.SetBool("action", false);
                        actionDone = false;
                    }
                    for (int i = 0; i < inventory.Length; i++)
                        if (inventory[i] != null)
                        {
                            if (i == 0)
                                inventory[0].transform.position = Vector3.MoveTowards(inventory[0].transform.position, transform.position, 0.1f);
                            else
                                inventory[i].transform.position = Vector3.MoveTowards(inventory[i].transform.position, inventory[i - 1].transform.position, 0.02f - 0.0025f * i);
                        }
        }
            
        
    }

    public void use(Type type, GameObject gameObject)
    {
        switch (type)
        {
            case Type.air:
                interactable.GetComponent<Items>().used = true;
                interactable.GetComponent<Items>().time = Time.time;
                interactable.GetComponent<CircleCollider2D>().enabled = false;
                oxygenBar.GetComponent<HealthBar>().currentOxygen = 100f;
                cdAction = 3;
                actionDone = true;
                GetComponentInParent<NearDeath>().black.color = new Color(GetComponentInParent<NearDeath>().black.color.r, GetComponentInParent<NearDeath>().black.color.g, GetComponentInParent<NearDeath>().black.color.b, 0f);
                GetComponentInParent<NearDeath>().red.color = new Color(GetComponentInParent<NearDeath>().red.color.r, GetComponentInParent<NearDeath>().red.color.g, GetComponentInParent<NearDeath>().red.color.b, 0f);
                break;
            case Type.chest:
                if (inventory[0])
                {
                    oxygenBar.GetComponent<HealthBar>().collectingOxygen = true;
                    Destroy(inventory[0]);
                    time = Time.time;
                    end = true;
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "key item":
                for (int i = 0; i < inventory.Length; i++)
                    if (inventory[i] == null)
                    {
                        inventory[i] = col.transform.gameObject;
                        inventory[i].transform.parent = null;
                        i = 7;
                    }
                col.enabled = false;
                break;
            case "air":
                interactable = col.gameObject;
                break;
            case "chest":
                interactable = col.gameObject;
                break;
            case "ball":
                oxygenBar.GetComponent<HealthBar>().collectingOxygen = true;
                end = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "air":
                interactable = null;
                break;
            case "chest":
                interactable = null;
                break;
        }
    }

    void changeScene()
    {
        SceneManager.LoadScene(nextPlanet.ToString(), LoadSceneMode.Single);
    }

    public enum Type
    {
        air,
        chest
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
