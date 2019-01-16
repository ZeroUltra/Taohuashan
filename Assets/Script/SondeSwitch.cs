using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class SondeSwitch : MonoBehaviour
{
    public bool isOn = false;
    public Sprite onSp, offSp;
    [Header("关闭state")]
    public SpriteState offState;
    [Header("打开state")]
    public SpriteState onState;

    public UnityEvent OnOff;
    public UnityEvent OnOpen;

    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(BtnOnClick);
        onState = btn.spriteState;
    }
    void BtnOnClick()
    {
        isOn = !isOn;
        if (isOn)
        {
            btn.spriteState = onState;
            btn.image.sprite = onSp;
            if (OnOpen != null)
                OnOpen.Invoke();
        }
        else
        {
            btn.spriteState = offState;
            btn.image.sprite = offSp;
            if (OnOff != null)
                OnOff.Invoke();
        }
    }
}
