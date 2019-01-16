

using UnityEngine;
using UnityEngine.UI;
using Wit.BaiduAip.Speech;
using DG.Tweening;
public class VoiceRecognition : MonoBehaviour
{
    public string APIKey = ""; //百度语音识别key
    public string SecretKey = ""; //密钥
    public Button StartButton;  //开始录音按钮
    public Button StopButton;   //停止录音按钮
    public Text DescriptionText;    //录音文本

    public GameObject endFX;
    public bool[] shijuStates;

    public RectTransform[] shijus;
    private AudioClip _clipRecord;
    private Asr _asr;


    void Start()
    {
        _asr = new Asr(APIKey, SecretKey);//实例
        StartCoroutine(_asr.GetAccessToken());

        StartButton.gameObject.SetActive(true);
        StopButton.gameObject.SetActive(false);
        DescriptionText.gameObject.SetActive(true);

        StartButton.onClick.AddListener(OnClickStartButton);//开始按钮监听
        StopButton.onClick.AddListener(OnClickStopButton);
        SceneController.Instance.bgm.SetActive(false);
    }

    private void OnClickStartButton()
    {
        StartButton.gameObject.SetActive(false);  //开始按钮按下 关闭开始按钮
        StopButton.gameObject.SetActive(true);     //打开停止按钮
        DescriptionText.text = "录音中...";

        _clipRecord = Microphone.Start(null, true, 30, 16000);//untiy 录音
    }

    private void OnClickStopButton()
    {
        StartButton.gameObject.SetActive(false);
        StopButton.gameObject.SetActive(false);
        DescriptionText.text = "转换中...";
        Microphone.End(null); //清空

        var data = Asr.ConvertAudioClipToPCM16(_clipRecord);  //转换成byte数据
        //将byte传入 然后转换
        StartCoroutine(_asr.Recognize(data, s =>
        {
            //转换回掉函数 文本=转换结果不为空且结果长度大于0 否则为“未识别声音”
            if (s.result == null)
            {
                DescriptionText.text = "未识别到声音";
                StartButton.gameObject.SetActive(true);
            }

            else
            {
                RecognitionResult(s.result[0]);
            }
        }));
    }
    public string ceshi;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RecognitionResult(ceshi);
        }
    }
    private void RecognitionResult(string str)
    {
        Debug.Log(str);
        StartButton.gameObject.SetActive(true);
        if (str.Contains("夹道朱楼一径斜"))
        {
            shijuStates[0] = true;
            shijus[0].DOSizeDelta(new Vector2(82, 528), 2.5f).SetEase(Ease.Linear);
            StartButton.GetComponentInChildren<Text>().text = "开始读第二句";
        }
        else if (str.Contains("王孙初御富平车"))
        {
            if (shijuStates[0])
            {
                shijuStates[1] = true;
                shijus[1].DOSizeDelta(new Vector2(82, 528), 2.5f).SetEase(Ease.Linear);
                StartButton.GetComponentInChildren<Text>().text = "开始读第三句";
            }
            else DescriptionText.text = "<color=red>朗读有错哦~</color>";
        }
        else if (str.Contains("青溪尽是辛夷树"))
        {
            if (shijuStates[0] && shijuStates[1])
            {
                shijuStates[2] = true;
                shijus[2].DOSizeDelta(new Vector2(82, 528), 2.5f).SetEase(Ease.Linear);
                StartButton.GetComponentInChildren<Text>().text = "开始读第四句";
            }
            else DescriptionText.text = "<color=red>朗读有错哦~</color>";
        }
        else if (str.Contains("不及东风桃李花"))
        {
            if (shijuStates[0] && shijuStates[1] && shijuStates[2])
            {
                shijuStates[3] = true;
                
                DescriptionText.gameObject.SetActive(false);
                StartButton.gameObject.SetActive(false);
                StopButton.gameObject.SetActive(false);
                shijus[3].DOSizeDelta(new Vector2(82, 528), 2.5f).SetEase(Ease.Linear).OnComplete(()=>
                {
                 
                    endFX.SetActive(true);
                    GameTools.WaitDoSomeThing(this, 5f, () => GameManager.Instance.LoadScene("4"));
                });

            }
            else DescriptionText.text = "<color=red>朗读有错哦~</color>";
        }
        else
        {
            DescriptionText.text = "<color=red>朗读有错哦~</color>";
        }

    }
}