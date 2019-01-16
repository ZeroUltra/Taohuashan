using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScrollIndex : MonoBehaviour
{

    [SerializeField]
    private int index = 1;
    public int Index
    {
        get { return index; }
        set
        {
            index = value;
        }
    }
    public void GetIndex()
    {
        GameTools.WaitDoSomeThing(this, 0.3f, () =>
        {
            Index = int.Parse(transform.GetChild(4).gameObject.name);
        });
    }

}
