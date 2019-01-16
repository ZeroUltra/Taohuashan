using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraFollow : MonoBehaviour
{
    [Header("X 限制")]
    public Vector2 clampX;
    [Header("Y 限制")]
    public Vector2 clampY;
    public Transform followTarget;//跟随目标
    public float offY;//位移差

    public float moveSpeed = 2f;
    public UnityEvent OnCameraMoveEnd;

    private Vector3 basePos;
    private bool isMove = false;
    private GameObject eventSystem;

    [SerializeField]
    private bool isGuideEndOrSkip = false;
    public bool IsGuideEndOrSkip
    {
        get
        {
            return isGuideEndOrSkip;
        }

        set
        {
            isGuideEndOrSkip = value;
        }
    }
    public bool ignorePreview = false;
    private void Start()
    {
        IsGuideEndOrSkip = isGuideEndOrSkip;
        basePos = transform.position;
        if (this.followTarget == null)
        {
            Debug.Log("没有目标对象");
            return;
        }
        if (!ignorePreview)
        {
            eventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>().gameObject;
            eventSystem.gameObject.SetActive(false);
            StartCoroutine(IEPreview());
        }
        else isMove = false;
    }

    private void LateUpdate()
    {
        if (!isMove)
        {
            if (IsGuideEndOrSkip)
            {
                Vector3 vector = transform.position;
                vector = this.followTarget.position + Vector3.up * this.offY;
                float x = Mathf.Clamp(vector.x, this.clampX.x, this.clampX.y);
                float y = Mathf.Clamp(vector.y, this.clampY.x, this.clampY.y);
                transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, -10f), 3 * Time.deltaTime);
            }
        }
    }
    /// <summary>
    /// 预览场景
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEPreview()
    {
        isMove = true;
        yield return new WaitForSeconds(1f);
        while (transform.position.x < clampX.y)
        {
            yield return null;
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
            if (transform.position.x > clampX.y)
            {
                transform.position = new Vector3(clampX.y, transform.position.y, -10);
                break;
            }
        }
        yield return new WaitForSeconds(0.8f);
        while (transform.position.x > clampX.x)
        {
            yield return null;
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
            if (transform.position.x < clampX.x)
            {
                transform.position = basePos;
                break;
            }
        }
        transform.position = basePos;
        eventSystem.gameObject.SetActive(true);

        if (OnCameraMoveEnd != null)
            OnCameraMoveEnd.Invoke();
        isMove = false;
    }
}
