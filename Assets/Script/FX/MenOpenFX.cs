using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class MenOpenFX : MonoBehaviour
{
    public UnityEvent OnSpriteNormal;
    public UnityEvent OnPlayEnd;
    private Transform leftDoor, rightDoor;

    public void Play()
    {
        OnSpriteNormal.Invoke();
        leftDoor = transform.GetChild(0);
        rightDoor = transform.GetChild(1);
        SpriteRenderer sp1 = leftDoor.GetComponent<SpriteRenderer>();
        SpriteRenderer sp2 = rightDoor.GetComponent<SpriteRenderer>();
        sp1.color = new Color32(255, 255, 255, 0);
        sp2.color = new Color32(255, 255, 255, 0);
        sp1.DOFade(1f, 1.3f);
        sp2.DOFade(1f, 1.3f).OnComplete(() =>
        {
           
            leftDoor.DOScaleX(0.4f, 2f);
            rightDoor.DOScaleX(0.4f, 2f).OnComplete(() =>
            {
                print("door end");
                if (OnPlayEnd != null) OnPlayEnd.Invoke();
            });

        });

    }



}
