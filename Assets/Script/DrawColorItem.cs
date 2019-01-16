using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
/// <summary>
/// 汲取颜色
/// </summary>
public class DrawColorItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("汲取的颜色或者设置时候的颜色")]
    public Color drawColor;
    public float drawDis = 3f;
    [Header("是否可以汲取")]
    public bool canDraw = true;
    public bool drawDoNothing = false;
    [Header("汲取的时候图片颜色")]
    public Color drawSpColor = new Color32(255, 255, 255, 170);//
    [Header("是否直接点击汲取颜色")]
    public bool clickDrawColor=false;
    public Color canNotDrawEndColor=Color.white;

    private bool isDrawEnd = false;
    private GameObject FXDraw;//汲取特效
    private GameObject FXSet;//光圈设置特效
    private DrawFX drawFX;
    private PlayerController player;
    private float downTime = 1f;
    private bool isDown = false;
    private Coroutine coroutine;

    //---------------
    private SpriteRenderer spriteRenderer;
    //------------------
    [Header("汲取结束")]
    public UnityEvent OnDrawEnd;
    [Header("设置颜色结束")]
    public UnityEvent OnSetEnd;
    public UnityEvent OnClickEnd;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        // if (canDraw)
        FXDraw = Resources.Load<GameObject>("FX/drawFX");
        //else
        // FXSet = Resources.Load<GameObject>("FX/LightFX");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void GetColor()
    {
       
        if (canDraw)//如果可以汲取
        {
            if (SceneController.Instance.uiButter.haveColor)
            {
                Debug.Log("蝴蝶有颜色");
                return;
            }
            Debug.Log("颜色汲取成功");
            SceneController.Instance.GetColorToTarget(drawColor);//赋值汲取颜色
            if (OnDrawEnd != null) OnDrawEnd.Invoke(); //事件
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickDrawColor) return;
        //判断汲取颜色与玩家距离
        if (Vector3.Distance(transform.position, player.transform.position) < drawDis)
        {
            //Debug.Log("开始汲取颜色");
            player.StopMove();
            isDown = true;
            downTime = Time.time;
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(IEDraw());
        }
    }
    /// <summary>
    /// 开始汲取颜色
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEDraw()
    {

        //克隆长按特效 播放
        drawFX = GameObject.Instantiate(FXDraw, this.transform.position, Quaternion.identity).GetComponent<DrawFX>();
        if (isDrawEnd)//如果已经汲取过或者有颜色
        {
            Debug.Log("已经汲取");
            drawFX.PlayError();
            yield break;
        }
        if (!canDraw&&drawDoNothing==false)
        {
            if (!SceneController.Instance.TrueColor(drawColor))
            {
                Debug.Log("颜色不对");
                drawFX.PlayError();
                yield break;
            }
        }
        drawFX.Play();
        if (spriteRenderer != null)
            spriteRenderer.color = drawSpColor;
        while (Time.time - downTime <= 1f && isDown)//是否一直在按下 死循环
        {
            yield return null;
        }
        //if (spriteRenderer != null)
        //   spriteRenderer.color = Color.white;

        if (drawDoNothing)
        {
            if (OnClickEnd != null) OnClickEnd.Invoke(); //事件
            yield break;
        }

        //按倒足够时间
        if (canDraw)//如果可以汲取
        {
            if (SceneController.Instance.uiButter.haveColor)
            {
                Debug.Log("蝴蝶有颜色");
               
                yield break;
            }
            Debug.Log("颜色汲取成功");
            isDrawEnd = true;
            drawFX.Stop();//汲取特效停止
            SceneController.Instance.GetColorToTarget(drawColor);//赋值汲取颜色
            if (OnDrawEnd != null) OnDrawEnd.Invoke(); //事件
        }
        else
        {
            Debug.Log("这个不能汲取,将汲取颜色设置到物体上");
            drawFX.Stop();//汲取特效停止
            bool isColorTrue = SceneController.Instance.SetColorToTarget(drawColor);//赋值汲取颜色
            Debug.Log("颜色是否正确：" + isColorTrue);
            //GameObject.Instantiate(FXSet, this.transform.position, Quaternion.identity);
            if (isColorTrue)
            {
                Debug.Log("设置事件");
                GetComponent<Collider2D>().enabled = false;
                if (OnSetEnd != null) OnSetEnd.Invoke(); //事件
            }
            else
            {
                drawFX.PlayError();
            }
        }

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        if (drawFX != null)
            drawFX.Stop();
        if (coroutine != null)
        {
            if (spriteRenderer != null)
                spriteRenderer.color = canNotDrawEndColor;
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

}
