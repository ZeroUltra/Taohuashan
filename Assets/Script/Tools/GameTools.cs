using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;
public class GameTools
{
    /// <summary>
    /// UI显示隐藏 渐变效果
    /// </summary>
    /// <param name="Go"></param>
    /// <param name="isOpen"></param>
    /// <param name="duartion"></param>
    /// <param name="openCallback"></param>
    /// <param name="closeCallback"></param>
    public static void ShowOrHideUI(GameObject Go, float duartion = 0.3f, UnityAction openCallback = null, UnityAction closeCallback = null)
    {
        Transform uiTrans = Go.transform;
        if (!Go.activeInHierarchy)
        {
            Go.SetActive(true);
            if (uiTrans.localScale != Vector3.zero)
                uiTrans.localScale = Vector3.zero;
            uiTrans.DOScale(Vector3.one, duartion).SetEase(Ease.OutBack).OnComplete(() => { if (openCallback != null) openCallback.Invoke(); });
        }
        else
        {

            uiTrans.DOScale(Vector3.zero, duartion).SetEase(Ease.InBack).OnComplete(() =>
            {
                Go.SetActive(false);
                if (closeCallback != null) closeCallback.Invoke();
            });
        }
       
    }
    public static void ShowOrHideGo(GameObject Go, bool isOpen, Vector3 scaleV3 = default(Vector3), float duartion = 0.4f, UnityAction openCallback = null, UnityAction closeCallback = null)
    {
        Transform Trans = Go.transform;
        if (scaleV3 == default(Vector3)) scaleV3 = Vector3.one;
        if (isOpen)
        {
            if (!Go.activeInHierarchy)
                Go.SetActive(true);
            if (Trans.localScale != Vector3.zero)
                Trans.localScale = Vector3.zero;
            Trans.DOScale(scaleV3, duartion).SetEase(Ease.OutBack).OnComplete(() => { if (openCallback != null) openCallback.Invoke(); });
        }
        else
        {

            Trans.DOScale(Vector3.zero, duartion).SetEase(Ease.InBack).OnComplete(() =>
            {
                Go.SetActive(false);
                if (closeCallback != null) closeCallback.Invoke();
            });
        }
    }
    /// <summary>
    /// canvasGroup spriteRender Image 渐隐
    /// </summary>
    /// <param name="uiGo"></param>
    /// <param name="hide">是否到0</param>
    /// <param name="duartion"></param>
    /// <param name="cleanCallback">回掉</param>
    /// <param name="oneCallback"></param>
    public static void FadeUI(GameObject uiGo, bool hide, float duartion = 1f, UnityAction cleanCallback = null, UnityAction oneCallback = null)
    {
        SpriteRenderer spriteRenderer = uiGo.GetComponent<SpriteRenderer>();
        CanvasGroup canvasGroup = uiGo.GetComponent<CanvasGroup>();
        Image image = uiGo.GetComponent<Image>();
        if (uiGo.gameObject.activeInHierarchy) uiGo.SetActive(true);
        if (canvasGroup != null)
        {
            if (hide)
            {
                canvasGroup.DOFade(0, duartion).SetEase(Ease.Linear).OnComplete(() =>
              {
                  if (canvasGroup.alpha <= 0.1f)
                  {
                      uiGo.gameObject.SetActive(false);
                      if (cleanCallback != null)
                          cleanCallback.Invoke();
                  }
              });
            }
            else
            {
                uiGo.gameObject.SetActive(true);
                canvasGroup.alpha = 0f;
                canvasGroup.DOFade(1, duartion).SetEase(Ease.Linear).OnComplete(() =>
                {
                    if (canvasGroup.alpha >= 0.9f)
                    {
                        if (oneCallback != null)
                            oneCallback.Invoke();
                    }
                });
            }

        }
        else if (image != null)
        {
            if (hide)
            {
                image.DOFade(0, duartion).SetEase(Ease.Linear).OnComplete(() =>
                {
                    if (image.color.a <= 0.1f)
                    {
                        uiGo.gameObject.SetActive(false);
                        if (cleanCallback != null)
                            cleanCallback.Invoke();
                    }
                });
            }
            else
            {

                uiGo.gameObject.SetActive(true);
                image.color = new Color(1, 1, 1, 0f);
                image.DOFade(1, duartion).SetEase(Ease.Linear).OnComplete(() =>
                {
                    if (image.color.a >= 0.9f)
                    {
                        if (oneCallback != null)
                            oneCallback.Invoke();
                    }
                });
            }
        }
        else if (spriteRenderer != null)
        {
            if (hide)
            {
                spriteRenderer.color = Color.white;
                spriteRenderer.DOFade(0, duartion).SetEase(Ease.Linear).OnComplete(() =>
                {
                    if (spriteRenderer.color.a <= 0.1f)
                    {
                        uiGo.gameObject.SetActive(false);
                        if (cleanCallback != null)
                            cleanCallback.Invoke();
                    }
                });
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, 0);
                uiGo.gameObject.SetActive(true);
                spriteRenderer.DOFade(1, duartion).SetEase(Ease.Linear).OnComplete(() =>
                {
                    if (spriteRenderer.color.a >= 0.9f)
                    {
                        if (oneCallback != null)
                            oneCallback.Invoke();
                    }
                });
            }
        }
    }

    /// <summary>
    /// 调用自创动画组件
    /// </summary>
    //public static void CallCustomAnima(GameObject go)
    //{
    //    CustomAnimation customAnimation = go.GetComponent<CustomAnimation>();
    //    if (customAnimation != null)
    //    {
    //        customAnimation.PlayAnimationOnce();
    //    }
    //    else
    //    {
    //        Debug.Log("没有自定义动画组件");
    //    }
    //}

    /// <summary>
    /// 高亮 静态发光 轮廓
    /// </summary>
    /// <param name="highGo"></param>
    /// <param name="isOn"></param>
    /// <param name="color">默认是黄色</param>
    //public static void HighLightConstantGo(GameObject highGo, bool isOn, Color color = default(Color))
    //{
    //    if (highGo.GetComponent<SpriteRenderer>() == null && highGo.GetComponent<MeshRenderer>() == null)
    //    {
    //        Debug.Log("没有renderer组件");
    //        return;
    //    }
    //    if (color == default(Color)) color = new Color32(241, 91, 15, 255);
    //    Highlighter h = highGo.GetComponent<Highlighter>() ?? highGo.AddComponent<Highlighter>();
    //    if (isOn)
    //        h.ConstantOn(color);
    //    else h.ConstantOff();


    //}
    /// <summary>
    /// 高光 闪烁发光 轮廓
    /// </summary>
    /// <param name="highGo"></param>
    /// <param name="isOn"></param>
    /// <param name="color">默认是黄色</param>
    //public static void HighLightFlashingGo(GameObject highGo, bool isOn, Color color = default(Color))
    //{
    //    if (highGo.GetComponent<SpriteRenderer>() == null && highGo.GetComponent<MeshRenderer>() == null)
    //    {
    //        Debug.Log("没有renderer组件");
    //        return;
    //    }
    //    if (color == default(Color)) color = new Color32(241, 91, 15, 255);
    //    Highlighter h = highGo.GetComponent<Highlighter>() ?? highGo.AddComponent<Highlighter>();
    //    if (isOn)
    //        h.FlashingOn(color, Color.clear, 0.7f);
    //    else h.FlashingOff();
    //}

    public static void WaitDoSomeThing(MonoBehaviour mono, float duartion, System.Action action)
    {
        mono.StartCoroutine(IEWait(mono, duartion, action));
    }
    private static IEnumerator IEWait(MonoBehaviour mono, float duartion, System.Action action)
    {
        yield return new WaitForSeconds(duartion);
        if (action != null) action();

    }

    /// <summary>
    /// 从resource加载物品
    /// </summary>
    /// <returns></returns>
    public static GameObject CreateGo(string path)
    {
        return GameObject.Instantiate(Resources.Load<GameObject>(path));
    }

    public static void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }


}
