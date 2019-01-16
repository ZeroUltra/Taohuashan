using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YunMove : MonoBehaviour {
    private float moveSpeed=50f;
    RectTransform rectTransform;
	void Start () {
        rectTransform = transform as RectTransform;
	}
	
	void Update () {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        if (rectTransform.anchoredPosition.x < -1991f)
        {
            rectTransform.anchoredPosition = new Vector2(1965f, -57.5f);
        }
	}
}
