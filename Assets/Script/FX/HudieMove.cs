using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
public class HudieMove : MonoBehaviour
{

    public UnityEvent OnFlyEnd;
    private DOTweenPath[] paths;
    void Start()
    {
        paths = GetComponentsInChildren<DOTweenPath>();
    }
    public void PlayTween()
    {
        GameTools.WaitDoSomeThing(this, 1.2f, () =>
        {
            for (int i = 0; i < paths.Length; i++)
            {
                paths[i].DOPlay();
            }
            GameTools.WaitDoSomeThing(this, 3f, () =>
             {
                 if (OnFlyEnd != null) OnFlyEnd.Invoke();
             });
        });

    }



}
