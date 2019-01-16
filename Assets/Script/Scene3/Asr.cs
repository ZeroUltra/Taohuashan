
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Wit.BaiduAip.Speech
{
    [Serializable]
    public class AsrResponse
    {
        public int err_no;
        public string err_msg;
        public string sn;
        public string[] result;
    }

    public class Asr : Base
    {
        private const string UrlAsr = "http://vop.baidu.com/server_api";

        public Asr(string apiKey, string secretKey) : base(apiKey, secretKey)
        {
        }
        /// <summary>
        /// 识别
        /// </summary>
        /// <param name="data"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IEnumerator Recognize(byte[] data, Action<AsrResponse> callback)
        {
			yield return PreAction ();//当有token的时候

			//if (tokenFetchStatus == Base.TokenFetchStatus.Failed) {
			//	Debug.LogError("Token fetched failed, please check your APIKey and SecretKey");
			//	yield break;
			//}

            var uri = string.Format("{0}?lan=zh&cuid={1}&token={2}", UrlAsr, SystemInfo.deviceUniqueIdentifier, Token);

            var form = new WWWForm();
            form.AddBinaryData("audio", data);
            var www = UnityWebRequest.Post(uri, form);
            www.SetRequestHeader("Content-Type", "audio/pcm;rate=16000");
            yield return www.Send();

            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.downloadHandler.text);
                //转换 返回json数据格式  反序列化AsrResponse类
                callback(JsonUtility.FromJson<AsrResponse>(www.downloadHandler.text));
            }
            else
                Debug.LogError(www.error);
        }

        /// <summary>
        /// 将Unity的AudioClip数据转化为PCM格式16bit数据
        /// </summary>
        /// <param name="clip"></param>
        /// <returns></returns>
        public static byte[] ConvertAudioClipToPCM16(AudioClip clip)
        {
            if (clip == null) return new byte[0];
            var samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);
            var samples_int16 = new short[samples.Length];

            for (var index = 0; index < samples.Length; index++)
            {
                var f = samples[index];
                samples_int16[index] = (short) (f * short.MaxValue);
            }

            var byteArray = new byte[samples_int16.Length * 2];
            Buffer.BlockCopy(samples_int16, 0, byteArray, 0, byteArray.Length);

            return byteArray;
        }
    }
}