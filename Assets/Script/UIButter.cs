using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIButter : MonoBehaviour
{
    public Image[] butterImgs;
    public bool haveColor = false;
    private Color butterColor=default(Color);

    private CustomAnimation[] customAnimations;
    public CustomAnimation[] CustomAnimations
    {
        get
        {
            if (customAnimations == null)
            {
                customAnimations = GetComponentsInChildren<CustomAnimation>();
            }
            return customAnimations;
        }
    }
    /// <summary>
    /// 设置颜色
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color)
    {
        butterColor = color;
        for (int i = 0; i < butterImgs.Length; i++)
        {
            butterImgs[i].color = butterColor;
        }
        for (int i = 0; i < CustomAnimations.Length; i++)
        {
            CustomAnimations[i].PlayAtSum(2);
        }
    }
    /// <summary>
    /// 获取颜色
    /// </summary>
    /// <returns></returns>
    public Color GetColor()
    {
        return butterColor;
    }

}
