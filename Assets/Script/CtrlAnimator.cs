using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlAnimator : MonoBehaviour {

    public float intervalTime;
    public float animaSpeed;
    Animator animator;
	IEnumerator Start () {
        animator = GetComponent<Animator>();
        animator.speed = animaSpeed;
        yield return new WaitForSeconds(intervalTime);
        animator.enabled = true;

	}
	
	
}
