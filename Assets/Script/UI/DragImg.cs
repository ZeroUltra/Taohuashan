using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class DragImg : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform thisRectTrans;
    private Vector3 basepos;

    void Start()
    {
        thisRectTrans = transform as RectTransform;
        basepos = thisRectTrans.anchoredPosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 outPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform as RectTransform, eventData.position, eventData.pressEventCamera, out outPos))
        {
            thisRectTrans.position = outPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //当结束拖动，要将物体归0,为了加一点缓冲效果
        //(1)可以使用dotween等补间动画插件,会减少很多
        thisRectTrans.DOAnchorPos(basepos, 0.5f);
    }

    //public void OnDrop(PointerEventData eventData)
    //{
    //    print(eventData.pointerEnter.gameObject);
    //}
}


