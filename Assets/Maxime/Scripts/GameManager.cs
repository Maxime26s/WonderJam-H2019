using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Planet nextPlanet;
    public Animator animator;
    public GameObject player, planete, right, left;
    public float speed = 5.0f;
    public bool playerCollideRight, playerCollideLeft;
    public bool grounded = true;
    public Direction direction;
    public float vi, vf, yi, yf, a, ti, tf;
    public bool action, actionDone;
    public float timerAction, cdAction, time, transparent;
    public GameObject[] inventory = new GameObject[5];
    public GameObject interactable, oxygenBar;
    public float talkCD;
    public TextMeshProUGUI text, nom;
    public GameObject panel, reponses;
    public TextMeshProUGUI[] textChoix;
    public Image face, black2;
    public bool end,end2;
    public bool answer = false;
    public float raceI, raceF;
    public GameObject start, final, wall, turbine;



    // Start is called before the first frame update
    void Start()
    {
        a = -9.8f;
        raceI = 0;
        raceF = 100;

        if (nextPlanet==Planet.Eau1|| nextPlanet == Planet.Eau2 || nextPlanet == Planet.Cave|| nextPlanet == Planet.Terre|| nextPlanet == Planet.None)
        {
            animator.SetBool("horse", true);
            if (nextPlanet == Planet.Eau1 || nextPlanet == Planet.Terre)
                inventory[0] = turbine;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (end2)
        {
            transparent += 0.33f * Time.deltaTime;
            black2.color = new Color(0f, 0f, 0f, transparent);
            if (Time.time - time >= 3)
                changeScene();
        }
        else
        {
            if (!action)
            {
                if (Input.GetKey(KeyCode.E) && grounded && Time.time - talkCD >= 0.3 && interactable != null)
                {
                    oxygenBar.GetComponent<HealthBar>().collectingOxygen = true;
                    talkCD = Time.time;
                    timerAction = Time.time;
                    action = true;
                    switch (interactable.tag)
                    {
                        case "air":
                            animator.SetBool("action", true);
                            use(Type.air, interactable);
                            break;
                        case "npc":
                            animator.SetBool("running", false);
                            use(Type.talk, interactable);
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
                if (Input.GetKey(KeyCode.E) && Time.time - talkCD >= 0.3)
                {
                    if (end)
                    {
                        panel.SetActive(false);
                        end2 =true;
                        text.SetText("");
                    }
                        
                    else
                    {
                    talkCD = Time.time;
                    actionDone = true;
                    }

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
                oxygenBar.GetComponent<HealthBar>().currentOxygen = 100f;
                cdAction = 3;
                actionDone = true;
                GetComponentInParent<NearDeath>().black.color = new Color(GetComponentInParent<NearDeath>().black.color.r, GetComponentInParent<NearDeath>().black.color.g, GetComponentInParent<NearDeath>().black.color.b, 0f);
                GetComponentInParent<NearDeath>().red.color = new Color(GetComponentInParent<NearDeath>().red.color.r, GetComponentInParent<NearDeath>().red.color.g, GetComponentInParent<NearDeath>().red.color.b, 0f);
                break;
            case Type.talk:
                bool required = false;
                if (nextPlanet == Planet.Pyramide && gameObject.GetComponent<DialogDisplayer>().nbTalk == 8)
                {
                    if (raceF - raceI <= 25)
                    {
                        gameObject.GetComponent<DialogDisplayer>().nbTalk++;
                        start.SetActive(false);
                        final.SetActive(false);
                        wall.SetActive(false);
                        panel.SetActive(true);
                        nom.SetText(gameObject.GetComponent<DialogDisplayer>().currentNPC.nom);
                        face.sprite = gameObject.GetComponent<DialogDisplayer>().currentNPC.npcSprite;
                        text.SetText("Huhuhu! Bravo Bravo! La pièce n’est pas sur moi cependant. Je l’ai entreposée dans la pyramide.");

                    }
                    else if(speed==0.9f){
                        panel.SetActive(true);
                        nom.SetText(gameObject.GetComponent<DialogDisplayer>().currentNPC.nom);
                        face.sprite = gameObject.GetComponent<DialogDisplayer>().currentNPC.npcSprite;
                        text.SetText("C’est le plus vite que vous pouvez faire !!!Mes marches de santé sont plus rapides que ça.Revenez me voir quand vous êtes prêt.");
                    }
                    else
                    {
                        animator.SetBool("horse", true);
                        animator.SetBool("running", false);
                        animator.SetBool("jumping", false);
                        animator.SetBool("action", false);
                        start.SetActive(true);
                        final.SetActive(true);
                        wall.SetActive(true);
                        speed = 0.9f;
                    
                    panel.SetActive(true);
                    nom.SetText(gameObject.GetComponent<DialogDisplayer>().currentNPC.nom);
                    face.sprite = gameObject.GetComponent<DialogDisplayer>().currentNPC.npcSprite;
                    text.SetText(gameObject.GetComponent<DialogDisplayer>().currentNPC.phrases[gameObject.GetComponent<DialogDisplayer>().nbTalk]);
                    }
                }

                else
                {
                    if (answer)
                    {
                        gameObject.GetComponent<DialogDisplayer>().nbTalk++;
                        answer = false;
                    }
                    if (gameObject.GetComponent<DialogDisplayer>().currentNPC.required[gameObject.GetComponent<DialogDisplayer>().nbTalk])
                    {
                        required = true;
                        int nb = 0;
                        for (int i = 0; i < gameObject.GetComponent<DialogDisplayer>().currentNPC.amount[gameObject.GetComponent<DialogDisplayer>().nbTalk]; i++)
                            if (inventory[i] != null)
                                nb++;
                        if (nb == gameObject.GetComponent<DialogDisplayer>().currentNPC.amount[gameObject.GetComponent<DialogDisplayer>().nbTalk])
                        {
                            gameObject.GetComponent<DialogDisplayer>().nbTalk++;
                            for (int i = nb - 1; i > -1; i--)
                            {
                                Destroy(inventory[i]);
                                inventory[i] = null;
                            }
                        }
                    }
                    panel.SetActive(true);
                    nom.SetText(gameObject.GetComponent<DialogDisplayer>().currentNPC.nom);
                    face.sprite = gameObject.GetComponent<DialogDisplayer>().currentNPC.npcSprite;
                    text.SetText(gameObject.GetComponent<DialogDisplayer>().currentNPC.phrases[gameObject.GetComponent<DialogDisplayer>().nbTalk]);
                    if (gameObject.GetComponent<DialogDisplayer>().currentNPC.reponse[gameObject.GetComponent<DialogDisplayer>().nbTalk])
                    {
                        reponses.SetActive(true);
                        for (int i = 0; i < 3; i++)
                            textChoix[i].SetText(gameObject.GetComponent<DialogDisplayer>().currentNPC.choix[i]);
                        gameObject.GetComponent<DialogDisplayer>().nbTalk--;
                    }
                    if (required && gameObject.GetComponent<DialogDisplayer>().currentNPC.required[gameObject.GetComponent<DialogDisplayer>().nbTalk])
                        gameObject.GetComponent<DialogDisplayer>().nbTalk--;
                    gameObject.GetComponent<DialogDisplayer>().nbTalk++;
                    if (gameObject.GetComponent<DialogDisplayer>().nbTalk == gameObject.GetComponent<DialogDisplayer>().currentNPC.phrases.Length)
                        end = true;
                    cdAction = 0;
                    actionDone = false;
                }


                break;
        }
    }

    void changeScene()
    {
        SceneManager.LoadScene(nextPlanet.ToString(), LoadSceneMode.Single);
    }

    public void wrongButton()
    {
        answer = false;
        reponses.SetActive(false);
        text.SetText("Mais non! Pas du tout! Les jeunes ne savent plus compter ou quoi?");
    }

    public void rightButton()
    {
        answer = true;
        reponses.SetActive(false);
        text.SetText("Hihihi! Félicitations! Maintenant que j’y pense, mon cheval aimerais bien se dégourdir les jambes.");
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
