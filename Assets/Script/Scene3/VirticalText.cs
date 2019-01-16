using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteInEditMode]
public class VirticalText : MonoBehaviour
{
    private Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }
    [ContextMenu("VerticalText")]
    private void VerticalText()
    {
        string temp = "";
        for (int i = 0; i < text.text.Length; i++)
        {
            temp += text.text[i].ToString() + "\n";
        }
        text.text = temp;
    }
}
