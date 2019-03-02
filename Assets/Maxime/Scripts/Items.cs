using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool used = false;
    public float time;

    // Update is called once per frame
    void Update()
    {
        if (used)
        {
            spriteRenderer.color -= new Color(0f, 0f, 0f, 0.33f*Time.deltaTime);
            if (Time.time - time >= 3)
                Destroy(this);
        }
    }
}
