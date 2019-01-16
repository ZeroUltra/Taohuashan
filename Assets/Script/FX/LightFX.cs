using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LightFX : MonoBehaviour {

	
	void Start () {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one,0.8f);
        transform.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
        transform.DOBlendableRotateBy(new Vector3(0, 0, 720f), 1.5f,RotateMode.LocalAxisAdd);
        Destroy(this.gameObject, 1f);
	}

	
}
