using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YunMove2 : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float clampX = 0;
    public Vector2 dir = Vector2.left;
    public Vector2 returnPos;


    void Update()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
        if (dir == Vector2.left)
        {
            if (transform.position.x < clampX)
            {
                transform.position = returnPos;
            }
        }
        else
        {
            if (transform.position.x > clampX)
            {
                transform.position = returnPos;
            }
        }

    }
}
