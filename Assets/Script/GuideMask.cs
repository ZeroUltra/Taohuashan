using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GuideMask : MonoBehaviour,ICanvasRaycastFilter
{
    public Image[] targets;//聚焦点
    public Image[] promptImgs;//提示ui
    public bool guideEnd = false;
    private Vector4 center;
    private Material material;
    private float diameter; // 直径
    private float current = 0f;
    private Vector3[] corners = new Vector3[4];
    private Canvas canvas;
    private bool isFocus = false;
    private float yVelocity = 0f;

    private int index = 0;
    private Image target;
    private bool isInit = false;

    private void Init()
    {
        if (isInit) return;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        targets = transform.GetChild(0).GetComponentsInChildren<Image>();
        promptImgs = transform.GetChild(1).GetComponentsInChildren<Image>();
        isInit = true;
    }

    private void Update()
    {
        if (isFocus)
        {
            float value = Mathf.SmoothDamp(current, diameter, ref yVelocity, 0.25f);
            if (Mathf.Abs(value - current) > 0.3f)
            {
                current = value;
                material.SetFloat("_Silder", current);
            }
            else
            {
                isFocus = false;
            }
        }
    }

    private Vector2 WordToCanvasPos(Canvas canvas, Vector3 world)
    {
        Vector2 position = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, world, canvas.GetComponent<Camera>(), out position);
        return position;
    }

    /// <summary>
    /// 聚焦
    /// </summary>
    public void FocusOn()
    {
        Init();
        if (index > targets.Length - 1) return;
        target = (targets[index]);
        ShowPrompt();
        index++;
        isFocus = true;
        target.rectTransform.GetWorldCorners(corners);
        diameter = Vector2.Distance(WordToCanvasPos(canvas, corners[0]), WordToCanvasPos(canvas, corners[2])) / 2f;

        float x = corners[0].x + ((corners[3].x - corners[0].x) / 2f);
        float y = corners[0].y + ((corners[1].y - corners[0].y) / 2f);

        Vector3 center = new Vector3(x, y, 0f);
        Vector2 position = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, center, canvas.GetComponent<Camera>(), out position);

        center = new Vector4(position.x, position.y, 0f, 0f);
        material = GetComponent<Image>().material;
        material.SetVector("_Center", center);

        (canvas.transform as RectTransform).GetWorldCorners(corners);
        for (int i = 0; i < corners.Length; i++)
        {
            current = Mathf.Max(Vector3.Distance(WordToCanvasPos(canvas, corners[i]), center), current);
        }
        material.SetFloat("_Silder", current);
    }

    /// <summary>
    /// 出现提示
    /// </summary>
    private void ShowPrompt()
    {
        if (index >= 1)
        {
            GameTools.FadeUI(promptImgs[index - 1].gameObject, true);
        }
        GameTools.FadeUI(promptImgs[index].gameObject, false, 2f);
    }

    public void GuideEnd()
    {
        GameTools.FadeUI(this.gameObject, true,1f);
        GameTools.FadeUI(promptImgs[index - 1].gameObject, true,1f);
    }

    //点击区域是否有效 true是不能点击
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (target == null || isFocus) return true;
        return !RectTransformUtility.RectangleContainsScreenPoint(target.rectTransform, sp, eventCamera);
    }

}
