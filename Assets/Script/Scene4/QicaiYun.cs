using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
/// <summary>
/// 七彩祥云
/// </summary>
public class QicaiYun : MonoBehaviour
{

    public UnityEvent OnMoveEnd;

    private Text talkTxt;
    private void Start()
    {
        talkTxt = transform.GetChild(1).GetComponentInChildren<Text>(true);
    }
    public void Move(Transform targetPos,float moveTime)
    {
        if (this.gameObject.activeInHierarchy == false) this.gameObject.SetActive(true);
        bool right = (transform.position.x - targetPos.position.x) < 0;
        Vector2 dir = right ? Vector2.right : Vector2.left;
        transform.localScale = right ? Vector3.one : new Vector3(-1, 1, 1);
        transform.DOMove(targetPos.position, moveTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (OnMoveEnd != null) OnMoveEnd.Invoke();
        });
    }
    public void NvzhuTalk(string str, System.Action callBack = null)
    {
        if (talkTxt.transform.parent.gameObject.activeInHierarchy == false)
            GameTools.FadeUI(talkTxt.transform.parent.gameObject, false, 1f);
        talkTxt.text = "";
        talkTxt.DOText(str, 4f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (callBack != null) callBack.Invoke();

        }).SetDelay(1f);
    }
    public void HideTalk()
    {
        GameTools.FadeUI(talkTxt.transform.parent.gameObject, true, 1f);
    }
}
