using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class ReReplaceImagepl
{

    [MenuItem("Tools/ChangeImg")]
    static void Start()
    {
        GameObject[] gos = Selection.gameObjects;
        Debug.Log("开始转换");
        for (int i = 0; i < gos.Length; i++)
        {
            Image img = gos[i].GetComponent<Image>();
            Sprite sp = img.sprite;
            //Sprite sp = gos[i].GetComponent<Image>().sprite;
            Object.DestroyImmediate(img);
            gos[i].AddComponent<SpriteRenderer>().sprite = sp;
        }
        Debug.Log("转换成功");
    }

    [MenuItem("Tools/开始游戏")]
    static void StartGame()
    {
        //SceneManager.LoadScene(0);
        // EditorApplication.LoadLevelInPlayMode(Application.dataPath + "Scenes/init.scene");
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/init.unity");

        EditorApplication.isPlaying = true;
    }
}
