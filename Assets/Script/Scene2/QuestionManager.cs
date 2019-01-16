using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestionManager : MonoBehaviour
{
    public List<GameObject> subjectList = new List<GameObject>();
    public List<GameObject> subjectList2 = new List<GameObject>();
    private GameObject errorPrompt, rightPrompt;
    public MEvent<bool> OnQuestionItemClick = new MEvent<bool>();
    void Awake()
    {
        //lanternRiddles = FindObjectOfType<LanternRiddles>();
        errorPrompt = transform.GetChild(1).gameObject;
        rightPrompt = transform.GetChild(2).gameObject;
        InitQusetion();
    }
    private void InitQusetion()
    {
        Transform subjectBg = transform.GetChild(0);

        subjectList.Clear();
        subjectList2.Clear();

        for (int i = 0; i < subjectBg.childCount; i++)
        {
            subjectList.Add(subjectBg.GetChild(i).gameObject);
            subjectList2.Add(subjectBg.GetChild(i).gameObject);
            subjectBg.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void OnItemClick(QuestionItem item)
    {
        if (item.isTrue)
        {
            //回答正确 提示正确 面板关闭 继续
            GameTools.FadeUI(rightPrompt, false, 0.2f, oneCallback: () =>
            {
                GameTools.WaitDoSomeThing(this, 1f, () =>
                {
                    rightPrompt.gameObject.SetActive(false);
                });
            });
            SetPlaneState(false);
            // lanternRiddles.OnQuestionItemClick(true);
            OnQuestionItemClick.Invoke(true);
        }
        else
        {
            InitQusetion();
            //回答错误 提示错误 面板关闭 重新开始
            GameTools.FadeUI(errorPrompt, false, 0.2f, oneCallback: () =>
            {
                GameTools.WaitDoSomeThing(this, 1f, () =>
                  {
                      errorPrompt.gameObject.SetActive(false);
                  });
            });
            SetPlaneState(false);
            //lanternRiddles.OnQuestionItemClick(false);
            OnQuestionItemClick.Invoke(false);
        }
    }
    public void SetPlaneState(bool isOpen)
    {
        if (isOpen)
        {
            GameTools.FadeUI(this.gameObject, false, 1f);

            for (int i = 0; i < subjectList2.Count; i++)
            {
                subjectList2[i].SetActive(false);
            }
            int index = Random.Range(0, subjectList.Count);
            subjectList[index].SetActive(true);
            subjectList.RemoveAt(index);

        }
        else
        {
            GameTools.WaitDoSomeThing(this, 0.7f, () =>
            {
                GameTools.FadeUI(this.gameObject, true, 0.8f);
            });
        }
    }

}
[System.Serializable]
public class MEvent<T> : UnityEvent<T>
{
}