using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    
    public float speed;
    
    public float amplitude;

    public float multiplier;

    public float oldTime;

    // Start is called before the first frame update
    void Start()
    {
        oldTime = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 translateVec = new Vector3(transform.position.x + amplitude, 0.0f, 0.0f)  * speed;
        transform.Translate(translateVec * Time.deltaTime);


       if (Time.time>=oldTime)
        {
            speed = -speed;
            oldTime += 2;
        }

    }
     
}
