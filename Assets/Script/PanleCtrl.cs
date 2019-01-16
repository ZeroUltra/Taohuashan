using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanleCtrl : MonoBehaviour
{
    /// <summary>
    /// 显示面板
    /// </summary>
    public void ShowPlane(GameObject showPlane)
    {
        this.gameObject.SetActive(true);
        GameTools.FadeUI(this.gameObject, false);
        GameTools.FadeUI(showPlane, false);
        int childCount = showPlane.transform.childCount;
        if (childCount > 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                GameTools.FadeUI(showPlane.transform.GetChild(i).gameObject, false);

            }
        }
    }
    /// <summary>
    /// 隐藏面板
    /// </summary>
    public void HidePlane(GameObject hidePlane)
    {

        GameTools.FadeUI(hidePlane, true);
        GameTools.FadeUI(this.gameObject, true);

        int childCount = hidePlane.transform.childCount;
        if (childCount > 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                GameTools.FadeUI(hidePlane.transform.GetChild(i).gameObject, true);

            }
        }
    }
    //public void FadeGo(UnityEngine.UI.Toggle toggle)
    //{
    //    GameTools.FadeUI(go, go.activeInHierarchy);
    //}
}
