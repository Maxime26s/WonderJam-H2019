using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlanet : MonoBehaviour
{


    public float speed;

    public float amplitude;

    public float multiplier;

    public float oldTime;

    public float secs;

    public float jsp;

    Vector2 startPos;

    // Start is called before the first frame update
    void Awake()
    {
        startPos = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 translateVec = new Vector3(transform.position.x + amplitude, 0.0f, 0.0f) * speed;
        transform.Translate (translateVec * Time.deltaTime);


        if (Time.time >= oldTime)
        {
            Vector3 teleportVec = new Vector3(2000.0f, 0.0f, 0.0f);
            transform.Translate(teleportVec);
            oldTime += secs;
        }
        */

        float newPos = Mathf.Repeat(Time.time * speed, jsp);
        transform.position = startPos + Vector2.right * newPos;
    }
}
