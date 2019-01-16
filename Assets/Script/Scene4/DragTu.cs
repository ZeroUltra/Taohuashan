using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using DG.Tweening;

public class DragTu : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject receiveImg;
    private RectTransform thisRectTrans;

    private Vector2 basePos;
    private Quaternion baseQuat;
    private Vector2 randanPos;
    public UnityEvent OnDragEnd;
    void Start()
    {
        //GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        thisRectTrans = transform as RectTransform;
        basePos = thisRectTrans.anchoredPosition;
        baseQuat = thisRectTrans.rotation;

        randanPos = new Vector2(Random.Range(-30f, 35f), Random.Range(0, 548f));
        thisRectTrans.DOAnchorPos(randanPos, 1.5f).SetDelay(2f);
        thisRectTrans.SetSiblingIndex(Random.Range(0,7));
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 outPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform as RectTransform, eventData.position, eventData.pressEventCamera, out outPos))
        {
            thisRectTrans.position = outPos;
            //if (eventData.pointerEnter != null)
            //{
            //    if (eventData.pointerEnter.transform.parent.name == "kuang")  //如果都碰到了框
            //    {
            //        RectTransform receiveRectTrans = eventData.pointerEnter.gameObject.transform as RectTransform;
            //        thisRectTrans.rotation = receiveRectTrans.rotation;
            //    }
            //    else
            //    {
            //        thisRectTrans.rotation = baseQuat;
            //    }
            //}
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject == receiveImg)
        {
            GetComponent<Image>().raycastTarget = false;
            thisRectTrans.DOAnchorPos(basePos, 0.3f);
            //thisRectTrans.DOScale(Vector3.one * 0.63f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
            //{
            //    GameTools.WaitDoSomeThing(this, 0.5f, () => receiveImg.gameObject.SetActive(false));
            //});

            OnDragEnd.Invoke();
        }
        else
        {
            thisRectTrans.DOAnchorPos(randanPos, 0.5f);
            thisRectTrans.DORotate(baseQuat.eulerAngles, 0.5f);
        }
    }
}
