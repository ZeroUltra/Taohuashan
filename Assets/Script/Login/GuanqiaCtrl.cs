using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GuanqiaCtrl : MonoBehaviour
{

    public Sprite[] sprites;
    public Transform choosePlane;
    public Button rightBtn, leftBtn, centerBtn;
    public Image title;
    public Image cloud;
    public LoginManager loginManager;
    private int Index=1;
    private void OnEnable()
    {
        rightBtn.onClick.AddListener(() => GetIndex(choosePlane));
        leftBtn.onClick.AddListener(() => GetIndex(choosePlane));
        int dicindex = 1;

        //初始化 关卡解锁信息
        foreach (var item in GameManager.Instance.DicScene.Values)
        {
            if (item)
            {
                choosePlane.Find(dicindex.ToString()).GetChild(0).gameObject.SetActive(false);
            }
            dicindex++;
        }
        //进入相关场景
        centerBtn.onClick.AddListener(() =>
        {
            if (GameManager.Instance.DicScene[Index])
            {
                GameManager.Instance.LoadScene(((Index).ToString()));
                loginManager.BgmFade();
            }
            else
            {
                Debug.Log("当前场景不能加载");
            }
        });
    }
    /// <summary>
    ///  两个点击按钮时候 获取关卡index
    /// </summary>
    public void GetIndex(Transform trans)
    {
        centerBtn.interactable = false;
        cloud.DOFade(0, 0.3f).OnComplete(() =>
        {
            cloud.DOFade(1, 0.7f);
            centerBtn.interactable = true;
        });
        GameTools.WaitDoSomeThing(this, 0.2f, () =>
        {
            Index = int.Parse(trans.GetChild(4).gameObject.name);
            title.color = Color.clear;
            title.sprite = sprites[Index - 1];
            title.DOColor(Color.white, 0.8f).SetEase(Ease.Linear);
        });
    }
}
