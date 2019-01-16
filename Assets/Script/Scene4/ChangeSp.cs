using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ChangeSp : MonoBehaviour
{
    public Sprite BWSp;
    private SpriteRenderer spriteRenderer;
    private Sprite baseSp;
    public Sprite BaseSp
    {
        get
        {
            return m_SpriteRenderer.sprite;
        }
        set
        {
            baseSp = value;
        }
    }
    public SpriteRenderer m_SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
            return spriteRenderer;
        }
        set
        {
            spriteRenderer = value;
        }
    }

    /// <summary>
    /// 变换到黑白色
    /// </summary>
    public void ChangeToBAW()
    {
       
        SpriteRenderer sp = null;
        if (transform.childCount == 0)
        {
            GameObject childSp = new GameObject("childSp");
            childSp.transform.SetParent(this.transform);
            childSp.transform.localPosition = Vector3.zero;
            childSp.transform.localEulerAngles = Vector3.zero;
            childSp.transform.localScale = Vector3.one;
            childSp.transform.SetAsFirstSibling();
            sp = childSp.AddComponent<SpriteRenderer>();
        }
        else
        {
            //if (transform.GetChild(0).GetComponent<ChangeSp>() == null)
            //{
            //    GameObject childSp = new GameObject("childSp");
            //    childSp.transform.SetParent(this.transform);
            //    childSp.transform.localPosition = Vector3.zero;
            //    childSp.transform.localEulerAngles = Vector3.zero;
            //    childSp.transform.localScale = Vector3.one;
            //    childSp.transform.SetAsFirstSibling();
            //    sp = childSp.AddComponent<SpriteRenderer>();
            //}
            sp = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        sp.sortingOrder = m_SpriteRenderer.sortingOrder;
        sp.sprite = BWSp;
        sp.color = new Color(1, 1, 1, 0);
        sp.DOFade(1, 2f).SetEase(Ease.Linear);

        m_SpriteRenderer.DOFade(0, 2f).SetEase(Ease.Linear);
    }
    public void ChangeToNormal()
    {
        SpriteRenderer childSp = transform.GetChild(0).GetComponent<SpriteRenderer>();
        childSp.DOFade(0, 3f).SetEase(Ease.Linear);
        m_SpriteRenderer.DOFade(1, 3f).SetEase(Ease.Linear);
        // GameTools.FadeUI(this.gameObject, false, 3);
    }
}
