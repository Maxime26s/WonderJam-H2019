using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Animator animator;
    public GameObject player, planete, right, left;
    public float speed = 5.0f;
    public bool playerCollideRight, playerCollideLeft;
    public bool grounded = true;
    public Direction direction;
    public float vi, vf, yi, yf, a, ti, tf;
    public bool action, actionDone;
    public float timerAction, cdAction;
    public GameObject[] inventory = new GameObject[5];
    public GameObject interactable, oxygenBar;
    public float talkCD;
    public TextMeshProUGUI text;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        a = -9.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!action)
        {
            if (Input.GetKey(KeyCode.E) && grounded&&Time.time-talkCD>=1&&interactable != null)
            {
                    talkCD = Time.time;
                    timerAction = Time.time;
                    action = true;
                    switch (interactable.tag)
                    {
                        case "air":
                            animator.SetBool("action", true);
                            use(Type.air,interactable);
                            break;
                        case "npc":
                            animator.SetBool("running", false);
                            use(Type.talk,interactable);
                            break;
                        default:
                            action = false;
                            break;
                }
            }
            else if (Input.GetKey(KeyCode.A) && !playerCollideRight)
            {
                planete.transform.eulerAngles = new Vector3(0, 0, planete.transform.eulerAngles.z - speed);
                direction = Direction.right;
                player.GetComponent<SpriteRenderer>().flipX = true;
                animator.SetBool("running", true);
                for (int i = 0; i < inventory.Length; i++)
                    if (inventory[i] != null)
                        inventory[i].transform.position = Vector3.MoveTowards(inventory[0].transform.position, right.transform.position, 0.0225f);

            }

            else if (Input.GetKey(KeyCode.D) && !playerCollideLeft)
            {
                planete.transform.eulerAngles = new Vector3(0, 0, planete.transform.eulerAngles.z + speed);
                direction = Direction.left;
                player.GetComponent<SpriteRenderer>().flipX = false;
                animator.SetBool("running", true);
                for (int i = 0; i < inventory.Length; i++)
                    if (inventory[i] != null)
                        inventory[i].transform.position = Vector3.MoveTowards(inventory[0].transform.position, left.transform.position, 0.0225f);
            }

            else
                animator.SetBool("running", false);

            if (Input.GetKey(KeyCode.Space) && grounded)
            {
                initFall(4);
                animator.SetBool("jumping", true);
            }

            if (!grounded)
            {
                vf = vi + a * (Time.time - ti);
                player.transform.position = new Vector3(0, yi + (vi + vf) * (Time.time - ti) / 2, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.E)&& Time.time - talkCD >= 1)
            {
                talkCD = Time.time;
                actionDone = true;
            }
            if (Time.time - timerAction > cdAction && actionDone)
            {
                panel.SetActive(false);
                text.SetText("");
                action = false;
                oxygenBar.GetComponent<HealthBar>().collectingOxygen = false;
                animator.SetBool("action", false);
                grounded = true;
                actionDone = false;
            }
        }

        for (int i = 0; i < inventory.Length; i++)
            if (inventory[i] != null)
            {
                if (i == 0)
                    inventory[0].transform.position = Vector3.MoveTowards(inventory[0].transform.position, player.transform.position, 0.02f);
                else
                    inventory[i].transform.position = Vector3.MoveTowards(inventory[i].transform.position, inventory[i - 1].transform.position, 0.02f - 0.0025f * i);
            }
    }

    public void initFall(float speed)
    {
        grounded = false;
        vi = speed;
        ti = Time.time;
        yi = player.transform.position.y;
    }

    public void use(Type type, GameObject gameObject)
    {
        switch (type)
        {
            case Type.air:
                interactable.GetComponent<Items>().used = true;
                interactable.GetComponent<Items>().time = Time.time;
                interactable.GetComponent<CircleCollider2D>().enabled = false;
                oxygenBar.GetComponent<HealthBar>().collectingOxygen = true;
                oxygenBar.GetComponent<HealthBar>().currentOxygen = 100f;
                cdAction = 3;
                GetComponentInParent<NearDeath>().black.color = new Color(GetComponentInParent<NearDeath>().black.color.r, GetComponentInParent<NearDeath>().black.color.g, GetComponentInParent<NearDeath>().black.color.b, 0f);
                GetComponentInParent<NearDeath>().red.color = new Color(GetComponentInParent<NearDeath>().red.color.r, GetComponentInParent<NearDeath>().red.color.g, GetComponentInParent<NearDeath>().red.color.b, 0f);
                break;
            case Type.talk:
                panel.SetActive(true);
                //text.SetText(gameObject.GetComponent<NPC>.npc.text[gameObject.GetComponent<NPC>.npc.nbTalk]);
                cdAction = 0;
                actionDone = false;
                break;
        }
    }

    public enum Direction
    {
        right,
        left
    }

    public enum Type
    {
        air,
        talk,
    }
}
