using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class Scene2Manager : SceneController
{

    public static bool isGuide = false;
    public void SetIsGuide(bool _isGuide)
    {
        isGuide = _isGuide;
    }
    public void LoadScene(string scenename)
    {
        GameManager.Instance.LoadScene(scenename);
    }
    public void DestoryGo(GameObject go)
    {
        DestroyImmediate(go);
    }

    //-----------------------------
    public GameObject duanqiao, narmalqiao;//断桥 正常桥
    public GameObject dengmi;           //灯笼灯谜
    public Transform[] endpos;
    private LanternRiddles lanternRiddles;
    protected override void Start()
    {
        base.Start();
        EventTriggerListener.Get(dengmi).onClick = OnDengClick;
        PlayerController.isLock = false;
        lanternRiddles = GetComponent<LanternRiddles>();
        lanternRiddles.OnPass.AddListener(OnDengmiPass);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDengmiPass();
        }
    }
    /// <summary>
    /// 当灯谜过关
    /// </summary>
    public void OnDengmiPass()
    {
        PlayerController.isLock = true;
        Player.transform.GetChild(1).gameObject.SetActive(false);
        GameTools.WaitDoSomeThing(this, 2f, () =>
         {
             GameTools.FadeUI(duanqiao, true, 1.5f);
             GameTools.FadeUI(narmalqiao, false, 1.5f);
         });

        GameTools.WaitDoSomeThing(this, 3.8f, () =>
        {
            Player.SetPlayerDir(true);
            Player.transform.GetChild(1).gameObject.SetActive(false);
            Player._PlayerState = PlayerState.Walk;
            Player.transform.DOMove(endpos[0].position, 0.8f).SetEase(Ease.Linear).OnComplete(() =>
             {
                 Player.transform.DOMove(endpos[1].position, 4f).SetEase(Ease.Linear).OnComplete(() =>
                 {
                     GameTools.WaitDoSomeThing(this, 1f, () => GameManager.Instance.LoadScene("3"));
                     Player.transform.DOMove(endpos[2].position, 2f).SetEase(Ease.Linear);
                 });
             });
        });
    }
    /// <summary>
    /// 灯笼灯谜点击 打开猜灯谜界面
    /// </summary>
    /// <param name="pointerEventData"></param>
    private void OnDengClick(PointerEventData pointerEventData)
    {
        Transform deng = pointerEventData.pointerCurrentRaycast.gameObject.transform;
        print(Vector3.Distance(deng.position, Player.transform.position));
        if (Vector3.Distance(deng.position, Player.transform.position) < 4f)
        {
            Debug.Log("点击到了灯");
            lanternRiddles.SetPlaneState(true);
        }
    }

    public void ShowWhite(GameObject white)
    {
        GameTools.FadeUI(white, false, 1.5f);
    }
}
