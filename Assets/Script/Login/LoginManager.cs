using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class LoginManager : MonoBehaviour
{
    public static bool isJump = false;

    private bool isClick = false;
    public CanvasGroup bgCanvasGroup;  //
    public Button startBtn;
    public AudioSource bgm;
    RectTransform bgRect;

    public bool IsClick
    {
        get
        {
            return isClick;
        }

        set
        {
            isClick = value;
        }
    }
    void Start()
    {
        bgRect = bgCanvasGroup.transform as RectTransform;
        if (isJump)
        {
            bgRect.anchoredPosition = new Vector2(0, 1080);
            return;
        }
        startBtn.onClick.AddListener(StartGame);
    }
    private void OnEnable()
    {
        bgm.volume = 0.6f;
    }
    public void StartGame()
    {
        bgRect.DOAnchorPos(new Vector2(0, 1080), 2.5f).SetEase(Ease.Linear);
    }
    ///// <summary>
    ///// 插画过场
    ///// </summary>
    ///// <returns></returns>
    //IEnumerator IEFloat()
    //{
    //    for (int i = 0; i < chahuaImgs.Length; i++)
    //    {
    //        yield return new WaitForSeconds(3f);
    //        chahuaImgs[i].DOFade(1f, 1.5f);
    //    }
    //    yield return new WaitForSeconds(3f);
    //    //选关界面
    //    bgRect.DOAnchorPosX(-1920f, 5f).SetEase(Ease.Linear);
    //}

    /// <summary>
    /// 点击选择关卡界面中间的UI  跳转场景
    /// </summary>
    /// <param name="index"></param>
    public void LoadScene(string scenename)
    {
        GameManager.Instance.LoadScene(scenename);
    }
    public void BgmFade()
    {
        DOTween.To(() => bgm.volume, x => bgm.volume = x, 0, 1.6f);
    }

    //private void OnGUI()
    //{
    //    if (GUILayout.Button("快进X2", GUILayout.Width(200), GUILayout.Height(50)))
    //    {
    //        Time.timeScale += 1;
    //    }
    //    if (GUILayout.Button("正常", GUILayout.Width(200), GUILayout.Height(50)))
    //    {
    //        Time.timeScale = 1;
    //    }
    //}
}


