using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class CustomAnimation : MonoBehaviour
{

    public enum SpriteType
    { Img, Sprite }
    public SpriteType spriteType;
    //-----------------------
    private Image img;//动画图片
    private SpriteRenderer spriteRenderer;
    //--------------------
    private int index;
    public int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
            if (index > sps.Length - 1)
            {
                if (loop)
                    index = 0;
                else
                {
                    autoPlay = false;
                    return;
                }
            }
            switch (spriteType)
            {
                case SpriteType.Img:
                    img.sprite = sps[index];

                    break;
                case SpriteType.Sprite:
                    spriteRenderer.sprite = sps[index];
                    break;
            }

        }
    }

    private float timer;

    public bool autoPlay = true;//是否自动播放
    public bool loop = true;
    public Sprite[] sps;
    public float intervalTime = 0.1f;//间隔时间

    void Start()
    {
        switch (spriteType)
        {
            case SpriteType.Img:
                img = GetComponent<Image>();
                break;
            case SpriteType.Sprite:
                spriteRenderer = GetComponent<SpriteRenderer>();
                break;
        }
    }

    private void Update()
    {
        if (autoPlay)
        {
            timer += Time.deltaTime;
            if (timer >= intervalTime)
            {
                timer = 0;
                Index++;
            }
        }
    }
    /// <summary>
    /// 播放固定次数
    /// </summary>
    public void PlayAtSum(int sum)
    {
        StartCoroutine(IEAnimation(sum));
    }
    public void Play()
    {
        autoPlay = true;
    }
    public void Stop()
    {
        autoPlay = false;
    }
    private IEnumerator IEAnimation(int sum)
    {
        for (int i = 0; i < sum * sps.Length; i++)
        {
            yield return new WaitForSeconds(intervalTime);
            Index++;
        }
        Index = 0;
    }
}
