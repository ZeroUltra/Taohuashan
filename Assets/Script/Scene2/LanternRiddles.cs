using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 灯谜
/// </summary>
public class LanternRiddles : MonoBehaviour
{
    public Transform dengmiPlane;

    public Transform introducePlane; //介绍面板
    public Transform dengmi;
    public Sprite[] sps;  //3种类型图片
    public QuestionManager questionManager;
    public UnityEvent OnPass;
    private List<LanternRiddlesItem> itemsList = new List<LanternRiddlesItem>();
    private List<LanternRiddlesItem> items2List = new List<LanternRiddlesItem>();
    private List<LanternRiddlesItem> itemClickList = new List<LanternRiddlesItem>();
    private bool isClick = false;
    private void Start()
    {
        items2List.AddRange(dengmi.GetComponentsInChildren<LanternRiddlesItem>());
        InitLantern();
        StartIntroduce();
        questionManager.OnQuestionItemClick.AddListener(OnQuestionItemClick);
        questionManager.gameObject.SetActive(false);
    }
    /// <summary>
    /// 初始化灯谜
    /// </summary>
    public void InitLantern()
    {
        itemsList.AddRange(dengmi.GetComponentsInChildren<LanternRiddlesItem>());
        List<LanternType> lanternList = new List<LanternType>() { LanternType.Type1, LanternType.Type2, LanternType.Type3 };
        for (int i = 0; i < lanternList.Count; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int randomIndex = Random.Range(0, itemsList.Count);
                itemsList[randomIndex].LanternType = lanternList[i];
                itemsList[randomIndex].SetClickItemSprite(sps[i]);
                itemsList.RemoveAt(randomIndex);
            }
        }
        for (int i = 0; i < items2List.Count; i++)
        {
            items2List[i].InitLanternItem();
        }
    }
    /// <summary>
    /// 介绍玩法
    /// </summary>
    private void StartIntroduce()
    {
        GameObject img1 = introducePlane.GetChild(0).gameObject;
        GameObject img2 = introducePlane.GetChild(1).gameObject;
        img1.gameObject.SetActive(true);
        img2.gameObject.SetActive(false);
        GameTools.WaitDoSomeThing(this, 3f, () =>
        {
            GameTools.FadeUI(img1.gameObject, true, 0.8f, cleanCallback: () =>
              {
                  GameTools.FadeUI(img2.gameObject, false, 1f);
                  GameTools.WaitDoSomeThing(this, 4f, () => GameTools.FadeUI(img2.gameObject, true, 1f));
              });
        });
    }
    /// <summary>
    /// 当灯item 点击
    /// </summary>
    /// <param name="item"></param>
    public void OnDengItemClick(LanternRiddlesItem item)
    {
        itemClickList.Add(item);
        if (itemClickList.Count >= 2)
        {
            if (item.LanternType != itemClickList[itemClickList.Count - 2].LanternType)
            {
                Debug.Log("点击出错");
                for (int i = itemClickList.Count - 1; i >= 0; i--)
                {
                    itemClickList[i].ResetLanternItem();
                    itemClickList.RemoveAt(i);
                }
            }
        }
        if (itemClickList.Count >= 3)
        {
            Debug.Log("点击正确出现灯谜");
            questionManager.SetPlaneState(true);
            for (int i = itemClickList.Count - 1; i >= 0; i--)
            {
                itemClickList.RemoveAt(i);
            }
        }
    }
    /// <summary>
    /// 展示猜灯谜界面
    /// </summary>
    public void SetPlaneState(bool isOpen)
    {
        if (isOpen)
        {
            this.enabled = true;
            if (isClick) return;
            GameTools.FadeUI(dengmiPlane.gameObject, false);
            isClick = true;
        }
        else
        {
            GameTools.FadeUI(dengmiPlane.gameObject, true,1.5f);
        }
    }
    /// <summary>
    /// 当问题Item点击
    /// </summary>
    public void OnQuestionItemClick(bool isRight)
    {
        if (isRight)
        {
            //如果问题回答正确 那么看是否答完了
            bool isPass = true;
            for (int i = 0; i < items2List.Count; i++)
            {
                if (items2List[i].isOpen == false)
                {
                    isPass = false;
                    break;
                }
            }
            if (isPass)
            {
                Debug.Log("过关");
                SetPlaneState(false);
                OnPass.Invoke();
            }
            else
            {
                Debug.Log("还有。。。。没过关");
            }
        }
        else
        {

            //如果回错误  重新洗牌
            for (int i = 0; i < items2List.Count; i++)
            {
                items2List[i].ResetImmit();
            }
            GameTools.WaitDoSomeThing(this, 1.7f, () =>
              {
                  InitLantern();
              });
        }
    }

}
public enum LanternType
{
    Type1, Type2, Type3
}
