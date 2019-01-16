using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ChuanCtrl : MonoBehaviour
{
    public PlayerController player;
    [Header("水里的花瓣")]
    public GameObject shuiliHua;
    [Header("飘落的花瓣")]
    public GameObject piaoluoHua;
    [Header("船里的红花")]
    public GameObject hongHua;
    public GameObject tipUI;
    public UnityEvent OnChuanMove1, OnChuanMove2;

    public void ChuanMove()
    {
        PlayerController.isLock = true;
        player.SetPlayerDir(true);
        transform.DOLocalMoveX(-2.3f, 3f).SetEase(Ease.Linear).OnComplete(() =>
         {
             player._PlayerState = PlayerState.Walk;
             player.transform.DOMoveX(-0.18f, 2f).SetEase(Ease.Linear).OnComplete(() =>
             {
                 OnChuanMove1.Invoke();
                 player._PlayerState = PlayerState.Idle;
                 player.transform.SetParent(this.transform);
                 transform.DOLocalMoveX(5.4f, 4f).SetEase(Ease.InOutSine).OnComplete(() =>
                 {
                     GameTools.FadeUI(tipUI, false, 1.3f);
                 });
             });
         });

    }
    /// <summary>
    /// 船移动到桥边
    /// </summary>
    public void ChuanMove2()
    {
        GameTools.WaitDoSomeThing(this, 2.1f, () =>
          {
              GameTools.FadeUI(tipUI, true, 1.3f);
              transform.DOLocalMoveX(17.5f, 4.5f).SetEase(Ease.InOutSine).OnComplete(() =>
                     {
                         player.transform.SetParent(null);
                         player._PlayerState = PlayerState.Walk;
                         player.transform.DOBlendableLocalMoveBy(Vector3.right * 1.5f, 1f).SetEase(Ease.Linear).OnComplete(()=>
                         {
                             player._PlayerState = PlayerState.Idle;
                             PlayerController.isLock = false;
                         });
                     });
          });
        if (OnChuanMove2 != null)
            OnChuanMove2.Invoke();
    }
    /// <summary>
    /// 船里的花慢慢消失
    /// </summary>
    public void ChuanHuaFade()
    {
        GameTools.WaitDoSomeThing(this, 1f, () =>
          {
              GameTools.FadeUI(hongHua, true, 3f);
              piaoluoHua.gameObject.SetActive(true);
          });

    }
    /// <summary>
    /// 水里的花开始展现
    /// </summary>
    public void ShuilihuaShow()
    {
        GameTools.WaitDoSomeThing(this, 1f, () =>
        {
            GameTools.FadeUI(shuiliHua, false, 5f, oneCallback: () => { ChuanMove(); });
            for (int i = 0; i < piaoluoHua.transform.childCount; i++)
            {
                GameTools.FadeUI(piaoluoHua.transform.GetChild(i).gameObject, true, duartion: 6f, cleanCallback: () => { piaoluoHua.gameObject.SetActive(false); });
            }
        });

    }

}
