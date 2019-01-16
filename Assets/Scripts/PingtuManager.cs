using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
public class PingtuManager : MonoBehaviour
{

    public Image xiaoguoImg;
    public GameObject dragPlane;
    public UnityEvent OnEnd;
    void Start()
    {
        GameTools.WaitDoSomeThing(this, 2f, () =>
        {
            xiaoguoImg.gameObject.SetActive(false);
            dragPlane.SetActive(true);
        });

    }

    private int sum = 0;
    /// <summary>
    /// 拖动计算 数量
    /// </summary>
    public void OnDragEndCountSum()
    {
        sum++;
        if (sum >= 12)
        {
            xiaoguoImg.transform.SetAsLastSibling();
            GameTools.FadeUI(dragPlane, true, 1.5f);
            GameTools.FadeUI(xiaoguoImg.gameObject, false, 1.5f);
            GameTools.WaitDoSomeThing(this, 2f, () =>
              {
                  xiaoguoImg.transform.DOScale(Vector3.one * 5, 3f).SetEase(Ease.Linear);
                  GameTools.WaitDoSomeThing(this, 2f, () =>
                   {
                       dragPlane.transform.parent.GetChild(0).gameObject.SetActive(false);
                       dragPlane.gameObject.SetActive(false);
                       GameTools.FadeUI(this.gameObject, true, 1.5f);
                       GameTools.FadeUI(xiaoguoImg.gameObject, true, 1.5f);
                       GameTools.WaitDoSomeThing(this, 1f, () =>
                       {
                           OnEnd.Invoke();
                       });
                   });
              });
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            sum = 12;
            OnDragEndCountSum();
        }
    }

}
