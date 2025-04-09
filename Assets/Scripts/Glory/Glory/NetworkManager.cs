
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Network Error: " + request.error);
            }
        }
    }
}
