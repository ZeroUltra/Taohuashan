using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LanternRiddlesItem : MonoBehaviour
{

    [SerializeField]
    private LanternType lanternType;
    public LanternType LanternType
    {
        get
        {
            return lanternType;
        }

        set
        {
            lanternType = value;
        }
    }
    public bool isOpen = false; //是否打开了
    private Image imgChild;
    private LanternRiddles lanternRiddles;

    private void Awake()
    {
        imgChild = transform.GetChild(0).GetComponent<Image>();
        imgChild.gameObject.SetActive(false);
        GetComponent<Button>().onClick.AddListener(BtnClick);
        lanternRiddles = Scene2Manager.Instance.GetComponent<LanternRiddles>();
    }


    private void BtnClick()
    {
        //如果点开图片状态为false 才能点击
        if (!imgChild.gameObject.activeInHierarchy)
        {
            GameTools.FadeUI(imgChild.gameObject, false);
            lanternRiddles.OnDengItemClick(this);
            isOpen = true;
        }
    }

    /// <summary>
    /// 设置灯谜item回到原始状态
    /// </summary>
    public void ResetLanternItem()
    {
        GameTools.FadeUI(imgChild.gameObject, false, 0.3f, oneCallback: () =>
           {
               GameTools.FadeUI(imgChild.gameObject, true, 0.3f);
           });
        isOpen = false;
    }
    public void ResetImmit()
    {
        GameTools.FadeUI(imgChild.gameObject, true, 0.3f);
        isOpen = false;
    }
    public void InitLanternItem()
    {
        GameTools.FadeUI(imgChild.gameObject, false, 0.8f, oneCallback: () =>
        {
            GameTools.FadeUI(imgChild.gameObject, true, 0.6f);
        });
        isOpen = false;
    }
    /// <summary>
    /// 设置item图片
    /// </summary>
    public void SetClickItemSprite(Sprite sp)
    {
        imgChild.sprite = sp;
    }
}
