using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FishCtrl : MonoBehaviour {

    public Sprite[] sprites;
    public List<DOTweenPath> fourPath = new List<DOTweenPath>();
    public List<DOTweenPath> fivePath = new List<DOTweenPath>();
    void Start () {
		
	}

    /// <summary>
    /// 鱼挡传
    /// </summary>
    public void FishAIBlockBoat()
    {
        foreach (var item in fourPath)
        {
            item.DOPlay();
        }
        foreach (var item in fivePath)
        {
            item.DOPlay();
        }
    }
    /// <summary>
    /// 鱼推船
    /// </summary>
    public void FishAIPushBoat()
    {
        fourPath[0].GetComponent<SpriteRenderer>().sortingOrder = fourPath[1].GetComponent<SpriteRenderer>().sortingOrder = 4;
        fourPath[2].GetComponent<SpriteRenderer>().sortingOrder = fourPath[3].GetComponent<SpriteRenderer>().sortingOrder = 4;
        fourPath[0].transform.DOLocalMoveX(-0.85f, 2f).SetEase(Ease.InOutSine);
        fourPath[1].transform.DOLocalMoveX(-0.75f, 2f).SetEase(Ease.InOutSine);
        fourPath[2].transform.DOLocalMove(new Vector2(-1.4f, 0.18f), 2f).SetEase(Ease.InOutSine);
        fourPath[3].transform.DOLocalMove(new Vector2(-1.81f, 0.19f), 2f).SetEase(Ease.InOutSine);

    }
    public void SetPartent(Transform trans)
    {
        this.transform.parent = trans;
    }
    public void FishChangeColor()
    {
        SpriteRenderer[] rens = transform.GetComponentsInChildren<SpriteRenderer>();
        foreach (var item in rens)
        {
            item.sprite = sprites[0];
            CustomAnimation customAnimation = item.GetComponent<CustomAnimation>();
            for (int i = 0; i < customAnimation.sps.Length; i++)
            {
                customAnimation.sps[i] = sprites[i];
            }
        }
    }
}
