using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneController : Singleton<SceneController>
{

    private PlayerController player;
    public PlayerController Player
    {
        get
        {
            if (player == null) player = FindObjectOfType<PlayerController>();
            return player;
        }
    }
    public GameObject bgm;
    public UIButter uiButter;//左上角蝴蝶

    protected virtual void Start()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        //GameManager.Instance.SetSceneKey(sceneName);
    }
    /// <summary>
    /// 重新开始当前关卡
    /// </summary>
    public virtual void ReStartCurrentGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// 回到最初选关场景
    /// </summary>
    public virtual void LoadInitScece()
    {
        LoginManager.isJump = true;
        GameManager.Instance.LoadScene("login");

    }
    /// <summary>
    /// 给ui蝴蝶和人设置获得颜色
    /// </summary>
    /// <param name="color"></param>
    public void GetColorToTarget(Color color)
    {
        uiButter.SetColor(color);
        uiButter.haveColor = true;
        Player.GetColor(color);
    }
    /// <summary>
    /// 将获得的颜色给物体
    /// </summary>
    /// <param name="color">物体需要颜色</param>
    public bool SetColorToTarget(Color needColor)
    {

        Color tempColor = uiButter.GetColor();
        if (needColor.Equals(tempColor))
        {
            uiButter.SetColor(Color.white);//给了就为空
            uiButter.haveColor = false;
            Player.GetColor(tempColor);
            return true;
        }
        return false;
    }
    public bool TrueColor(Color needColor)
    {
        Color tempColor = uiButter.GetColor();
        return needColor.Equals(tempColor);
    }

    public void ShowOrHideUI(GameObject go)
    {
        GameTools.FadeUI(go, go.activeInHierarchy);
    }
    
}
public enum SceneType
{
    Scene02,Scene03,Scene04,Scene05
}