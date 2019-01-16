    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeOrdersort : MonoBehaviour {

    SpriteRenderer playerSP;
	void Start () {
        playerSP = GetComponentInChildren<SpriteRenderer>();
    }
	
	void LateUpdate () {
        if (transform.position.y < -3.81f)
            playerSP.sortingOrder = 8;
        if (transform.position.y > -3.81f)
            playerSP.sortingOrder =3;
        if (transform.position.y > 0.35f)
            playerSP.sortingOrder = 1;
    }
}
