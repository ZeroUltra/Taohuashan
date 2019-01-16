using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening;
public class Scene04Manager : SceneController
{

    [Header("开场女主出场")]
    public CameraFollow cameraFollow;
    public QicaiYun qicaiYun;
    public Transform pos1, pos2, posyun;
    public ChangeSp[] changeSps;
    public GameObject colorsGo;
    [Header("拼图界面")]
    public GameObject pingtuPlane;
    [Header("七彩炫云")]
    public GameObject clickQicaiYun;
    [Header("彩虹")]
    public GameObject caihong;
    public GameObject nvzhu;
    [Header("地面点击")]
    public BoxCollider2D groundCollider;
    public GameObject canvas;
    public AnswerPlane answerPlane;
    public AudioClip changeBgm;
    // public 
    public UnityEvent OnNvzhuMoveEnd;


    protected override void Start()
    {
        base.Start();
        cameraFollow.OnCameraMoveEnd.AddListener(QicaiYunMove);//摄像机移动结束 然后女主出现
        EventTriggerListener.Get(clickQicaiYun).onClick = ClickYun;
    }
    /// <summary>
    /// 女主第一次出场给颜色
    /// </summary>
    private void QicaiYunMove()
    {
        qicaiYun.Move(pos1, 5f);//移动到指定地点
        qicaiYun.OnMoveEnd.AddListener(() =>
        {
            qicaiYun.NvzhuTalk("没有你的日子，整个世界失去了色彩", () =>
            {
                ChangeColor(false);
                GameTools.WaitDoSomeThing(this, 3.5f, () => qicaiYun.NvzhuTalk("现在给你几种颜色，如果你真的是我的意中人，就请点亮我的世界吧         ", () =>
                {
                    qicaiYun.HideTalk();
                    GameTools.FadeUI(colorsGo, false, 1.5f, oneCallback: () =>
                    {
                        qicaiYun.OnMoveEnd.RemoveAllListeners();
                        GameTools.WaitDoSomeThing(this, 1f, () => qicaiYun.Move(pos2, 3f));
                        GameTools.WaitDoSomeThing(this, 4f, () =>
                        {
                            qicaiYun.gameObject.SetActive(false);
                            OnNvzhuMoveEnd.Invoke();
                        });
                    });
                }));
            });
        });
    }

    /// <summary>
    /// 改变场景中所有的图片颜色 
    /// </summary>
    /// <param name="normal">是否彩色</param>
    private void ChangeColor(bool normal)
    {
        if (normal)
        {
            for (int i = 0; i < changeSps.Length; i++)
            {
                changeSps[i].ChangeToNormal();
            }
        }
        else
        {
            for (int i = 0; i < changeSps.Length; i++)
            {
                changeSps[i].ChangeToBAW();
            }
        }
    }


    private int sum = 0;
    /// <summary>
    /// 赋予颜色 统计
    /// </summary>
    public void GetColorSum()
    {
        sum++;
        if (sum >= 3)
        {
            GameTools.WaitDoSomeThing(this, 1f, () =>
            {
                ChangeColor(true);
                GameTools.FadeUI(clickQicaiYun, false, 1.5f);
            });
        }
    }
    public void ClickYun(PointerEventData eventData)
    {
        clickQicaiYun.GetComponent<BoxCollider2D>().enabled = false;
        Transform deng = eventData.pointerCurrentRaycast.gameObject.transform;
        print(Vector3.Distance(deng.position, Player.transform.position));
        if (Vector3.Distance(deng.position, Player.transform.position) < 5f)
        {
            /// 出现拼图界面
            GameTools.FadeUI(pingtuPlane, false, 1.5f);
        }

    }
    /// <summary>
    /// 出现彩虹 女主 男主走上去
    /// </summary>
    public void OpenCaiHongAndNvzhu()
    {
        GameTools.FadeUI(pingtuPlane, false, 1.5f);
        GameTools.FadeUI(nvzhu.gameObject, false, 3f);
        GameTools.FadeUI(caihong.gameObject, false, 3f);
        groundCollider.enabled = false;
        GameTools.WaitDoSomeThing(this, 3.2f, () =>
          {
              Player.PlayerMove(posyun.position, () =>
             {
                 GameTools.FadeUI(Player.GetComponentInChildren<PlayerShadow>().gameObject, true);//隐藏倒影
                 GameTools.FadeUI(canvas, true);//画布隐藏
                 Player.SetPlayerDir(true);  //设置向右

                 //女主提问
                 //GameTools.FadeUI(nvzhu.transform.GetChild(0).gameObject, false, 1f);
                 //  Text text = nvzhu.transform.GetChild(0).GetComponentInChildren<Text>();
                 //  text.DOText("我的意中人会记得我们的过往吗", 2f).SetEase(Ease.Linear).OnComplete(() =>
                 // {
                 //    //出现答题面板
                 //});
                 NvzhuTing("我的意中人会记得我们的过往    ", () => answerPlane.Show(true));
                 //人物移动路径
                 //Vector3[] waypoints = new[] { new Vector3(4.35f, -1.032007f, 0f), new Vector3(5.164424f, -0.04210535f, 0f), new Vector3(5.829236f, 0.5752205f, 0f), new Vector3(6.763138f, 1.12923f, 0f), new Vector3(7.950302f, 1.335005f, 0f), new Vector3(9.171f, 1.413819f, 0f), new Vector3(10.31902f, 1.256247f, 0f), new Vector3(11.46704f, 0.6934913f, 0f), new Vector3(12.06689f, 0.2795507f, 0f), new Vector3(12.67462f, 0.2455046f, 0f), new Vector3(13.01521f, 0.2813561f, 0f) };
                 //Player.transform.DOPath(waypoints,10,PathType.CatmullRom).SetEase(Ease.Linear);
                 //Player.transform.DOScale(Vector3.one * 0.312236f, 8.5f).SetEase(Ease.Linear).OnComplete(()=>
                 //{
                 //    Camera mainCamera = Camera.main;
                 //    mainCamera.GetComponent<CameraFollow>().enabled = false;
                 //    mainCamera.transform.DOMove(new Vector3(13.3f, 1.5f, -10), 4f).SetEase(Ease.OutQuad);
                 //    mainCamera.DOOrthoSize(2f, 4f).SetEase(Ease.OutQuad);
                 //});

             });
          });
    }
    /// <summary>
    /// 回答正确
    /// </summary>
    public void AnswerOK()
    {
        GameTools.WaitDoSomeThing(this, 0.5f, () =>
         {

             NvzhuTing("真的是你！！！！       ", () =>
             {
                 GameTools.FadeUI(nvzhu.transform.GetChild(0).gameObject, true, 0.6f);
                 GetComponent<AudioSource>().clip = changeBgm;
                 GetComponent<AudioSource>().Play();
                 GameTools.WaitDoSomeThing(this, 2f, () =>
                 {
                     Vector3[] waypoints = new[] { new Vector3(4.35f, -1.032007f, 0f), new Vector3(5.164424f, -0.04210535f, 0f), new Vector3(5.829236f, 0.5752205f, 0f), new Vector3(6.763138f, 1.12923f, 0f), new Vector3(7.950302f, 1.335005f, 0f), new Vector3(9.171f, 1.413819f, 0f), new Vector3(10.31902f, 1.256247f, 0f), new Vector3(11.46704f, 0.6934913f, 0f), new Vector3(12.06689f, 0.2795507f, 0f), new Vector3(12.67462f, 0.2455046f, 0f), new Vector3(13.01521f, 0.2813561f, 0f) };
                     Player.transform.DOPath(waypoints, 10, PathType.CatmullRom).SetEase(Ease.Linear);
                     Player.transform.DOScale(Vector3.one * 0.312236f, 8.5f).SetEase(Ease.Linear).OnComplete(() =>
                     {
                         Camera mainCamera = Camera.main;
                         mainCamera.GetComponent<CameraFollow>().enabled = false;
                         mainCamera.transform.DOMove(new Vector3(13.3f, 1.5f, -10), 4f).SetEase(Ease.OutQuad);
                         mainCamera.DOOrthoSize(2f, 4f).SetEase(Ease.OutQuad);
                     });

                 });
             });
         });
    }
    /// <summary>
    /// 亭子女主说话
    /// </summary>
    public void NvzhuTing(string str, System.Action callback = null)
    {
        GameTools.FadeUI(nvzhu.transform.GetChild(0).gameObject, false, 1f);
        Text text = nvzhu.transform.GetChild(0).GetComponentInChildren<Text>();
        text.text = "";
        text.DOText(str, 2.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            //出现答题面板
            if (callback != null) callback.Invoke();
        }).SetDelay(0.5f);
    }
    public void NvzhuStopTalk()
    {
        GameTools.FadeUI(nvzhu.transform.GetChild(0).gameObject, true, 0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Time.timeScale = 1;
        if (Input.GetKeyDown(KeyCode.W))
            Time.timeScale = 3;
        if (Input.GetKeyDown(KeyCode.E))
        {
            sum = 3;
            GetColorSum();
        }

    }
}
