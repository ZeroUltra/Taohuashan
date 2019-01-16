using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : MonoBehaviour {
    public float moveSpeed=1f;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * moveSpeed*Time.deltaTime);
	}
}
