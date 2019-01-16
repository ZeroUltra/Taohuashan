using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCtrl : MonoBehaviour
{

    public VideoPlayer startVideoPlayer, loopVideoPlayer;
    public GameObject btnStart;
    IEnumerator Start()
    {
        startVideoPlayer.loopPointReached += VideoCtrl_loopPointReached;
        yield return new WaitForSeconds(40f);
        GameTools.FadeUI(btnStart, false);//显示开始按钮
        yield return new WaitForSeconds(9.5f);
        loopVideoPlayer.Play();
    }

    private void VideoCtrl_loopPointReached(VideoPlayer source)
    {


        startVideoPlayer.Stop();
        startVideoPlayer.gameObject.SetActive(false);
    }

}
