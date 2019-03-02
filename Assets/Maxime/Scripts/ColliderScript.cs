using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    public GameObject gameManager;

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "floor":
                if (!gameManager.GetComponent<GameManager>().grounded)
                {
                    gameManager.GetComponent<GameManager>().grounded = true;
                    gameManager.GetComponent<GameManager>().vf = 0;
                    gameManager.GetComponent<GameManager>().animator.SetBool("jumping", false);
                }
                Debug.Log("floor");
                break;
            case "ceiling":
                if (!gameManager.GetComponent<GameManager>().grounded)
                {
                    gameManager.GetComponent<GameManager>().initFall(0);
                    Debug.Log("ceiling2");
                }
                Debug.Log("ceiling");
                break;
            case "wall":
                if (gameManager.GetComponent<GameManager>().direction == GameManager.Direction.left)
                    gameManager.GetComponent<GameManager>().playerCollideLeft = true;
                if (gameManager.GetComponent<GameManager>().direction == GameManager.Direction.right)
                    gameManager.GetComponent<GameManager>().playerCollideRight = true;
                Debug.Log("wall");
                break;
            case "key item":
                for (int i = 0; i < gameManager.GetComponent<GameManager>().inventory.Length; i++)
                    if (gameManager.GetComponent<GameManager>().inventory[i] == null)
                    {
                        gameManager.GetComponent<GameManager>().inventory[i] = col.transform.gameObject;
                        gameManager.GetComponent<GameManager>().inventory[i].transform.parent = null;
                        i = 7;
                    }
                col.enabled = false;
                break;
            case "air":
                gameManager.GetComponent<GameManager>().interactable = col.gameObject;
                break;
        }

        Debug.Log("Troll");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "floor":
                if (gameManager.GetComponent<GameManager>().grounded)
                {
                    gameManager.GetComponent<GameManager>().grounded = false;
                    gameManager.GetComponent<GameManager>().vi = 1;
                    gameManager.GetComponent<GameManager>().ti = Time.time;
                    gameManager.GetComponent<GameManager>().yi = gameManager.GetComponent<GameManager>().player.transform.position.y;
                }
                Debug.Log("floor");
                break;
            case "wall":
                if (gameManager.GetComponent<GameManager>().playerCollideLeft)
                    gameManager.GetComponent<GameManager>().playerCollideLeft = false;
                if (gameManager.GetComponent<GameManager>().playerCollideRight)
                    gameManager.GetComponent<GameManager>().playerCollideRight = false;
                Debug.Log("wall");
                break;
            case "air":
                gameManager.GetComponent<GameManager>().interactable = null;
                break;
        }
        Debug.Log("Trol2l");
    }
}
