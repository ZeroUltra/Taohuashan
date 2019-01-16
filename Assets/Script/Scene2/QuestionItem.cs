using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestionItem : MonoBehaviour
{
    public bool isTrue = false;

    private QuestionManager questionManager;
    void Start()
    {
        questionManager = GetComponentInParent<QuestionManager>();
        GetComponent<Button>().onClick.AddListener(BtnClick);
    }

    private void BtnClick()
    {
        questionManager.OnItemClick(this);   
    }
}
