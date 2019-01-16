using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KaihuaFX : MonoBehaviour
{

    public UnityEvent OnOpenEnd;
    private CustomAnimation[] followOpens;
    void Start()
    {
        followOpens = GetComponentsInChildren<CustomAnimation>();
    }
    public void FollowOpen()
    {
        for (int i = 0; i < followOpens.Length; i++)
        {
            followOpens[i].PlayAtSum(1);
        }
        GameTools.WaitDoSomeThing(this, 0.6f, () =>
        {
            if (OnOpenEnd != null) OnOpenEnd.Invoke();
        });
    }


}
