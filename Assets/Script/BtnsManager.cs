using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BtnsManager : MonoBehaviour
{
    private Button xingnangBtn, restartBtn, backBtn;

    private CanvasGroup cg;
    private RectTransform rectTrans;
    public RectTransform RectTrans
    {
        get
        {
            if (rectTrans == null)
                rectTrans = transform as RectTransform;
            return rectTrans;
        }
    }
    public CanvasGroup Cg
    {
        get
        {
            if (cg == null)
                cg = GetComponent<CanvasGroup>();
            return cg;
        }
    }
  
    public void ShowOrHideBtns()
    {
        Init();
        if (this.gameObject.activeInHierarchy)
        {
            RectTrans.DOSizeDelta(new Vector2(205f, 0f), 0.7f).OnComplete(() => this.gameObject.SetActive(false));
            Cg.DOFade(0, 0.8f);
        }
        else
        {
            this.gameObject.SetActive(true);
            RectTrans.DOSizeDelta(new Vector2(205f, 930f), 0.7f);
            Cg.DOFade(1, 0.8f);
        }
    }
    private void Init()
    {
        if (xingnangBtn == null || restartBtn == null || backBtn == null)
        {
            Transform btns = transform.GetChild(0);
            xingnangBtn = btns.GetChild(0).GetComponent<Button>();
            restartBtn = btns.GetChild(2).GetComponent<Button>();
            backBtn = btns.GetChild(3).GetComponent<Button>();
        }
    }

}
