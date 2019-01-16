
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
/// <summary>
/// 玩家移动区域
/// </summary>
public class PlayerMoveAeraCtrl : MonoBehaviour,IPointerClickHandler{

    private PlayerController player;
    private GameObject clickFx;
    public bool canClick = true;
    [Header("点击到云")]
    public MyEvent OnClickGround;//点击到云
    void Start () {
        player = FindObjectOfType<PlayerController>();
        clickFx = Resources.Load<GameObject>("FX/clickFx");
    }
	
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canClick) return;
       // Debug.Log("点击到区域_"+eventData.pointerCurrentRaycast.worldPosition);
        Vector2 clickpos = eventData.pointerCurrentRaycast.worldPosition;
        player.PlayerMove(clickpos);
        //点击特效消失
        Destroy(Instantiate(clickFx, clickpos, Quaternion.identity), 1f);
        if (OnClickGround != null)
            OnClickGround.Invoke(clickpos);
    }
}
[System.Serializable]
public class MyEvent : UnityEvent<Vector2>
{
}
