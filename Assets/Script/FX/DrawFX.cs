using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 汲取颜色特效
/// </summary>
public class DrawFX : MonoBehaviour
{
    public Sprite errorSp;
    public AudioClip[] clips;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Transform center;
    bool isInit = false;

    Tweener scaleTw, fadeTw, rotateTw;
    private void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        center = transform.GetChild(0);
        spriteRenderer.color = new Color32(255, 255, 255, 0);
        transform.localScale = Vector3.zero;
        isInit = true;
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(bool stopAudio=false)
    {
        if (!isInit) Init();
        scaleTw = transform.DOScale(Vector3.one, 0.2f);
        fadeTw = spriteRenderer.DOFade(1, 0.2f).OnComplete(() =>
          {
              rotateTw = center.DOBlendableRotateBy(Vector3.back * 360f, 0.8f, RotateMode.LocalAxisAdd);
              if (!stopAudio)
              {
                  audioSource.clip = clips[0];//正确声音
                  audioSource.Play();
              }
          });
    }
    public void Stop()
    {
        scaleTw.Kill();
        rotateTw.Kill();
        fadeTw.Kill();
        transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => Destroy(this.gameObject));
    }

    /// <summary>
    /// 不能汲取或者已经汲取过
    /// </summary>
    public void PlayError()
    {
        Play(true);
        GameTools.WaitDoSomeThing(this, 0.4f, () =>
        {
            spriteRenderer.sprite = errorSp;
            scaleTw.Kill();
            fadeTw.Kill();
            rotateTw.Pause();
            rotateTw.PlayBackwards();

            audioSource.clip = clips[1];//错误声音
            audioSource.Play();
        });
        GameTools.WaitDoSomeThing(this, 0.8f, () =>
        {
            transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => Destroy(this.gameObject));
        });
    }
}
