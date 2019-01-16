using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerFirst : UIManager
{
    public Transform guidePlane;
    private Button guideBtn, skipBtn;

    public GuideMask guideMask;
    public GameObject endGuidePlane;
    [Header("四个提示按钮")]
    public GameObject butsFourBtnsMask;
    public GameObject butsFour;
    [Header("得到红色走到门前")]
    public GameObject endPrompt;
    protected override void Start()
    {
        base.Start();
        guideBtn = guidePlane.GetChild(0).GetComponent<Button>();
        skipBtn = guidePlane.GetChild(1).GetComponent<Button>();
        guideBtn.onClick.AddListener(GuideBtnClick);
        skipBtn.onClick.AddListener(SkipBtnClick);
    }

    private void SkipBtnClick()
    {
        Scene01Manager.isGuide = false;
    }

    /// <summary>
    /// 当点击引导按钮 聚焦
    /// </summary>
    private void GuideBtnClick()
    {
        GuideFocusOn();
    }
    /// <summary>
    /// 引导聚焦
    /// </summary>
    public void GuideFocusOn()
    {
        if ((Scene01Manager.Instance as Scene01Manager).cameraFollow.IsGuideEndOrSkip) return;
        GameTools.WaitDoSomeThing(this, 0.3f, () =>
        {
            guideMask.gameObject.SetActive(true);
            guideMask.FocusOn();
        });
    }
    /// <summary>
    /// 等待一定时间在聚焦
    /// </summary>
    /// <param name="duartion"></param>
    public void WaitTimeGuideFocusOn(float duartion)
    {
        if (!Scene01Manager.isGuide) return;
        GameTools.WaitDoSomeThing(this, duartion, () =>
          {
              GuideFocusOn();
          });
    }
    /// <summary>
    /// 引导结束 菜单按钮
    /// </summary>
    public void GuideEndPose()
    {
        if (guideMask.guideEnd) return;
        if (!guideMask.gameObject.activeInHierarchy) return;
        guideMask.guideEnd = true;
        guideMask.GuideEnd();
        //显示菜单下面的四个按钮以及mask
        GameTools.WaitDoSomeThing(this, 1f, () =>
        {
            GameTools.FadeUI(butsFourBtnsMask, false, 1.2f);
            GameTools.FadeUI(butsFour, false, 1.2f);
        });
        GameTools.WaitDoSomeThing(this, 4f, () =>
        {
            GameTools.FadeUI(butsFourBtnsMask, true, 1f);
            GameTools.FadeUI(butsFour, true, 1f);
            GameTools.WaitDoSomeThing(this, 0.9f, () =>
            {
                Scene01Manager.Instance.ShowOrHideUI(endGuidePlane);
            });
            GameTools.WaitDoSomeThing(this, 4f, () =>
             {
                 Scene01Manager.Instance.ShowOrHideUI(endGuidePlane);
             });
        });
    }
    /// <summary>
    /// 场景1展示最后的提示
    /// </summary>
    public void ShowEndPrompt()
    {
        GameTools.WaitDoSomeThing(this, 0.6f, () =>
        {
            Scene01Manager.Instance.ShowOrHideUI(endPrompt);
        });
        GameTools.WaitDoSomeThing(this, 4f, () =>
        {
            Scene01Manager.Instance.ShowOrHideUI(endPrompt);
        });
    }
}
