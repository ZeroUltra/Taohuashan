using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 行囊
/// </summary>
public class XingnangManager : Singleton<XingnangManager>
{

    public Toggle[] togs;
    public GameObject[] tips;
    void Start()
    {
        for (int i = 0; i < togs.Length; i++)
        {
            int index = i;
            togs[i].onValueChanged.AddListener((ison) =>
            {

                GameTools.FadeUI(tips[index], !ison,0.4f);
            });
           // togs[i].gameObject.SetActive(false);
        }
        foreach (var item in GameManager.Instance.DicRember)
        {
            if (item.Value)
            {
                for (int i = 0; i < togs.Length; i++)
                {
                    if (togs[i].name == item.Key)
                    {
                        togs[i].gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 获得物品
    /// </summary>
    /// <param name="name"></param>
    public void GetObj(string name)
    {
       
        for (int i = 0; i < togs.Length; i++)
        {
            if (togs[i].name == name)
            {
                print("获得了：" + name);
                togs[i].gameObject.SetActive(true);
                GameManager.Instance.SetObjKey(name);
            }
        }
    }
    public void Show(bool isOpen)
    {
        GameTools.FadeUI(this.gameObject, !isOpen, 0.5f);
        if (isOpen == false)
        {
            GameTools.FadeUI(this.gameObject.transform.parent.gameObject, true, 0.5f);
        }
        else
        {
            this.gameObject.transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            this.gameObject.transform.parent.gameObject.SetActive(true);
        }
    }

}
