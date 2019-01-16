using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Dictionary<int, bool> DicScene = new Dictionary<int, bool>()   //场景打开
                                                { {1,true },{2,false },{ 3,false},
                                                { 4,false},{ 5,false} };
    public Dictionary<string, bool> DicRember = new Dictionary<string, bool>()
                                                { {"卷",false },{"伞",false },{"扇",false}};

    private void Start()
    {
       // Screen.SetResolution(1920, 1080,true);
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        //场景设置
        List<int> sceneKeyList = new List<int>(DicScene.Keys);
        foreach (var item in sceneKeyList)
        {
            if (PlayerPrefs.GetString(item.ToString()) != string.Empty)
            {
                DicScene[item] = bool.Parse(PlayerPrefs.GetString(item.ToString()));
            }
        }
        foreach (var item in DicScene)
        {
            Debug.Log("场景完成度：" + item.Key + "___" + item.Value);
        }
        //SetSceneKey("2");
        //SetSceneKey("3");
        //SetSceneKey("4");
        //记忆碎片设置
        PlayerPrefs.SetString("伞", "false");
        PlayerPrefs.SetString("卷", "false");
        PlayerPrefs.SetString("扇", "false");
        List<string> remberKeyList = new List<string>(DicRember.Keys);
        foreach (var item in remberKeyList)
        {
            if (PlayerPrefs.GetString(item.ToString()) != string.Empty)
            {
                DicRember[item] = bool.Parse(PlayerPrefs.GetString(item.ToString()));
            }
        }
        foreach (var item in DicRember)
        {
            Debug.Log("物品：" + item.Key + "___" + item.Value);
        }
        SceneManager.LoadScene("login");
    }
    /// <summary>
    /// 设置场景被打开
    /// </summary>
    /// <param name="sceneName"></param>
    public void SetSceneKey(string sceneName)
    {
        DicScene[int.Parse(sceneName)] = true;
        PlayerPrefs.SetString(sceneName, DicScene[int.Parse(sceneName)].ToString());
    }
    /// <summary>
    /// 设置记忆碎片
    /// </summary>
    /// <param name="objName">卷  伞  扇</param>
    public void SetObjKey(string objName)
    {
        DicRember[objName] = true;
        PlayerPrefs.SetString(objName, DicRember[objName].ToString());
    }
    public bool HasObjKey(string objname)
    {
        return bool.Parse(PlayerPrefs.GetString(objname));
    }
    /// <summary>
    /// 重置新玩游戏
    /// </summary>
    public void ResetGame()
    {
        DicScene = new Dictionary<int, bool>()   //场景打开
                                                { {1,true },{2,false },{ 3,false},
                                                { 4,false},{ 5,false} };
        DicRember = new Dictionary<string, bool>()
                                                { {"卷",false },{"伞",false },{"扇子",false}};
        PlayerPrefs.DeleteAll();
    }




    #region LoadScene
    /// <summary>
    /// 点击选择关卡界面中间的UI  跳转场景
    /// </summary>
    /// <param name="index"></param>
    public void LoadScene(string scenename)
    {
        //跳转界面出现 根据跳转的场景不同 背景也不同
        StartCoroutine(IELoadScene(scenename));
    }
    IEnumerator IELoadScene(string scenename)
    {
        //loadUI.gameObject.SetActive(true);
        RectTransform loadUI = Instantiate(Resources.Load<GameObject>("Go/LoadUI")).transform as RectTransform;
        loadUI.transform.SetParent(FindObjectOfType<Canvas>().transform);
        loadUI.localScale = Vector3.one;
        loadUI.offsetMax = loadUI.offsetMin = Vector2.zero;
        loadUI.anchorMax = Vector2.one;
        loadUI.anchorMin = Vector2.zero;
        //根据场景名字设置图片
        loadUI.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Pass/" + scenename);
        loadUI.SetAsLastSibling();
        StartCoroutine(IEProgress(loadUI.transform.GetChild(1).GetChild(0).GetComponent<Image>()));
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scenename);
    }
    IEnumerator IEProgress(Image proimg)
    {
        if (proimg.type != Image.Type.Filled) Debug.LogError("error!!!");
        proimg.fillAmount = 0f;
        while (proimg.fillAmount < 1f)
        {
            proimg.fillAmount += (1f / 3f) * Time.deltaTime;
            yield return null;
        }
    }
    #endregion
}
