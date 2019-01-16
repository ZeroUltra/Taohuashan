using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using DG.Tweening;
public class DragShi : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject receiveImg;
    private RectTransform thisRectTrans;
    private Coroutine coroutine = null;
    private Vector2 basePos;
    private Quaternion baseQuat;
    public UnityEvent OnDragEnd;
    void Start()
    {
        thisRectTrans = transform as RectTransform;
        basePos = thisRectTrans.anchoredPosition;
        baseQuat = thisRectTrans.rotation;
    }

    public void OnDrag(PointerEventData eventData)
    {
        (SceneController.Instance as Scene3Manager).OnDragShiHideHand();
        Vector3 outPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform as RectTransform, eventData.position, eventData.pressEventCamera, out outPos))
        {
            thisRectTrans.position = outPos;
            if (eventData.pointerEnter != null)
            {
                if (eventData.pointerEnter.transform.parent.name=="kuang")  //如果都碰到了框
                {
                    RectTransform receiveRectTrans = eventData.pointerEnter.gameObject.transform as RectTransform;
                    thisRectTrans.rotation = receiveRectTrans.rotation;
                }
                else
                {
                    thisRectTrans.rotation = baseQuat;
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject == receiveImg)
        {
            GetComponent<Image>().raycastTarget = false;
            thisRectTrans.DOAnchorPos((receiveImg.transform as RectTransform).anchoredPosition, 0.3f);
            thisRectTrans.DOScale(Vector3.one * 0.63f, 0.5f).SetEase(Ease.OutBack).OnComplete(()=>
            {
                GameTools.WaitDoSomeThing(this, 0.5f, () => receiveImg.gameObject.SetActive(false));
            });
           
            OnDragEnd.Invoke();
        }
        else
        {
            thisRectTrans.DOAnchorPos(basePos, 0.5f);
            thisRectTrans.DORotate(baseQuat.eulerAngles, 0.5f);
        }
    }
}