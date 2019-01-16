using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWithPlayer : MonoBehaviour {

    public Transform player;
    private Vector3 off;
	void Start () {
        off = transform.position - player.position;
	}
	
	
	void LateUpdate () {
        transform.position = Vector3.Lerp(transform.position, player.position+off, 5 * Time.deltaTime);
	}
}
