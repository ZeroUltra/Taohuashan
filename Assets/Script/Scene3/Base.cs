
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Wit.BaiduAip.Speech
{
    /// <summary>
    /// 用户解析token的json数据
    /// </summary>
    [Serializable]
    class TokenResponse
    {
        public string access_token = null;
    }

    public class Base
	{
		protected enum TokenFetchStatus
		{
			NotFetched,
			Fetching,
			Success,
			Failed
		}

		public string SecretKey { get; private set; }

		public string APIKey { get; private set; }

		public string Token { get; private set; }

		protected TokenFetchStatus tokenFetchStatus = TokenFetchStatus.NotFetched;

		public Base (string apiKey, string secretKey)
		{
			APIKey = apiKey;
			SecretKey = secretKey;
		}
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
		public IEnumerator GetAccessToken ()
		{
			Debug.Log ("开始获取token");
			tokenFetchStatus = TokenFetchStatus.Fetching;

			var uri =
				string.Format (
					"https://openapi.baidu.com/oauth/2.0/token?grant_type=client_credentials&client_id={0}&client_secret={1}",
					APIKey, SecretKey);
			var www = UnityWebRequest.Get(uri);
			yield return www.Send();

			if (string.IsNullOrEmpty (www.error)) {
                Debug.Log(www.downloadHandler.text);
				var result = JsonUtility.FromJson<TokenResponse> (www.downloadHandler.text);
				Token = result.access_token;
				Debug.Log ("token 获取成功");
				tokenFetchStatus = TokenFetchStatus.Success;
			} else {
				Debug.LogError (www.error);
				Debug.LogError("token 获取失败 检查key和密钥");
				tokenFetchStatus = TokenFetchStatus.Failed;
			}
		}

		protected IEnumerator PreAction ()
		{
			if (tokenFetchStatus == TokenFetchStatus.NotFetched) {
				Debug.Log ("没有token，开始获取");
				yield return GetAccessToken ();
			}

			if (tokenFetchStatus == TokenFetchStatus.Fetching)
				Debug.Log ("token仍在被提取，等待中。。。");

			while (tokenFetchStatus == TokenFetchStatus.Fetching) {
				yield return null;
			}
		}
	}
}