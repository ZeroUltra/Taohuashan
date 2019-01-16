using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerPlane : Singleton<AnswerPlane> {

	
	void Start () {
		
	}
    public void BtnClick(bool isRight)
    {
        if (!isRight)
        {
            Show(false);
            (Scene04Manager.Instance as Scene04Manager).NvzhuTing("你不是我的意中人,你走吧。。。",callback:()=>
            {
                (Scene04Manager.Instance as Scene04Manager).NvzhuStopTalk();
                (Scene04Manager.Instance as Scene04Manager).groundCollider.enabled = true;
            });
        }
    }
    public void ShowNextAnswer(GameObject go)
    {

        GameTools.FadeUI(go,false,0.5f);
    }

    public void Show(bool isOpen)
    {
       
        if (isOpen == false)
        {
            GameTools.FadeUI(this.gameObject.transform.parent.gameObject, true, 0.5f);
            GameTools.FadeUI(this.gameObject, true, 0.5f);
        }
        else
        {
            this.gameObject.transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            this.gameObject.transform.parent.gameObject.SetActive(true);
            //this.gameObject.SetActive(true);
            GameTools.FadeUI(this.gameObject, false, 0.8f);
        }
      
    }

}
