using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Fx_Get : MonoBehaviour
{

    private void OnEnable()
    {
        Destroy(this.transform.parent.gameObject, 0.8f);
    }
}
