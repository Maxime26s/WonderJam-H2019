using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCharles : MonoBehaviour
{
    public GameObject gameManager;

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "floor":
                if (!gameManager.GetComponent<PlayerController2>().grounded)
                {
                    gameManager.GetComponent<PlayerController2>().grounded = true;
                    gameManager.GetComponent<PlayerController2>().vf = 0;
                }
                Debug.Log("floor");
                break;
            case "ceiling":
                if (!gameManager.GetComponent<PlayerController2>().grounded)
                {
                    gameManager.GetComponent<PlayerController2>().initFall(0);
                    Debug.Log("ceiling2");
                }
                Debug.Log("ceiling");
                break;
            case "wall":
                if (gameManager.GetComponent<PlayerController2>().direction == PlayerController2.Direction.left)
                    gameManager.GetComponent<PlayerController2>().playerCollideLeft = true;
                if (gameManager.GetComponent<PlayerController2>().direction == PlayerController2.Direction.right)
                    gameManager.GetComponent<PlayerController2>().playerCollideRight = true;
                Debug.Log("wall");
                break;
        }

        Debug.Log("Troll");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "floor":
                if (gameManager.GetComponent<PlayerController2>().grounded)
                {
                    gameManager.GetComponent<PlayerController2>().grounded = false;
                    gameManager.GetComponent<PlayerController2>().vi = 1;
                    gameManager.GetComponent<PlayerController2>().ti = Time.time;
                    gameManager.GetComponent<PlayerController2>().yi = gameManager.GetComponent<PlayerController2>().player.transform.position.y;
                }
                Debug.Log("floor");
                break;
            case "wall":
                if (gameManager.GetComponent<PlayerController2>().playerCollideLeft)
                    gameManager.GetComponent<PlayerController2>().playerCollideLeft = false;
                if (gameManager.GetComponent<PlayerController2>().playerCollideRight)
                    gameManager.GetComponent<PlayerController2>().playerCollideRight = false;
                Debug.Log("wall");
                break;
        }
        Debug.Log("Trol2l");
    }
}
