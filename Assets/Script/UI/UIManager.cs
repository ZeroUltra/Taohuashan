using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI场景管理
/// </summary>
public class UIManager : Singleton<UIManager>{
   
    [Tooltip("菜单按钮")]
    public Button menuBtn;
    private BtnsManager btnsManager;//菜单下的几个按钮
    protected virtual void Start()
    {
        btnsManager = menuBtn.GetComponentInChildren<BtnsManager>(true);
        menuBtn.onClick.AddListener(MenuBtnClick);
    }

    private void MenuBtnClick()
    {
        btnsManager.ShowOrHideBtns();
    }


}
