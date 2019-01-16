using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class YunClick : MonoBehaviour, IPointerClickHandler
{

    public PlayerController player;
    // public float dis = 0.5f;
    public bool isOnCloud = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        print("点击到云");
        if (Vector3.Distance(player.transform.position, this.transform.position) < 0.8f)
        {
            if (!isOnCloud)
            {
                PlayerController.isLock = true;
                player.transform.DOMoveY(-2.08f, 1f).SetEase(Ease.Linear).OnComplete(()=>
                {
                    isOnCloud = true;
                });
            }
           
        }
    }

    /// <summary>
    /// 当在云上点击地板的时候
    /// </summary>
    public void OnCloudClick(Vector2 pos)
    {
        if (isOnCloud)
        {
            player.transform.DOMoveY(-2.844f, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                PlayerController.isLock = false;
                player.PlayerMove(pos);
                isOnCloud = false;
            });
        }
    }
}
