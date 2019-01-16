using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01Manager : SceneController {

    public CameraFollow cameraFollow;
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


    public GameObject san;
    protected override void Start()
    {
        base.Start();
        //if(GameManager.Instance.HasObjKey())
    }
}
