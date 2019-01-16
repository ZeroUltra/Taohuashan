using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class Scene3Manager : SceneController
{
    public GameObject voicePlane;
    public GameObject hand;
    public GameObject tip;
    public PlayClickMove playerClick;

    public Transform zhuanpan;


    private int sumZhuanpan = 0;  //转盘已经汲取数量
    private int sumDrag;          //拖动诗的数量
    /// <summary>
    /// 检查颜色是否赋予完毕
    /// </summary>
    public void CheckColorEnd()
    {
        sumZhuanpan++;
        if (sumZhuanpan >= 3)
        {
            playerClick.enabled = false;
            zhuanpan.DOBlendableLocalRotateBy(Vector3.forward * 360, 3f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                zhuanpan.GetChild(0).DOBlendableLocalMoveBy(Vector3.left * 0.3f, 1f);
                zhuanpan.GetChild(1).DOBlendableLocalMoveBy(Vector3.right * 0.3f, 1f);
                zhuanpan.GetChild(2).DOBlendableLocalMoveBy(Vector3.up * 0.3f, 1f);
                GameTools.FadeUI(voicePlane, false, 2f);
            });
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sumZhuanpan = 3;
            CheckColorEnd();
        }
    }
    /// <summary>
    /// 当拖动诗 把手隐藏
    /// </summary>
    public void OnDragShiHideHand()
    {
        if (hand.activeInHierarchy)
        { GameTools.FadeUI(hand, true); }
    }

    /// <summary>
    /// 检测诗句是否拖动完毕
    /// </summary>
    public void OnDragEnd()
    {
        sumDrag++;
        if (sumDrag >= 4)  //如果诗句都拖动完毕 
        {
            GameTools.FadeUI(tip, true);  //隐藏提示
            (voicePlane.transform.GetChild(1) as RectTransform).DOAnchorPos(new Vector2(-320,30), 1.2f).SetDelay(0.7f);  //扇子往中间移动
            voicePlane.GetComponent<VoiceRecognition>().enabled = true;     //开启录音
        }
    }
}
