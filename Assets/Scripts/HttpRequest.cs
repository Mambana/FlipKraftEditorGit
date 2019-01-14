using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class HttpRequest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Get(string url, Action<string> callback)
    {
        StartCoroutine(_Get(url, callback));
    }

    public void Post(string url, Dictionary<string, string> arguments, Action<string> callback)
    {
        StartCoroutine(_Post(url, arguments, callback));
    }

    public void Put(string url, byte[] data, Action<string> callback)
    {
        StartCoroutine(_Put(url, data, callback));
    }

    private IEnumerator _Get(string url, Action<string> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            callback(www.downloadHandler.text);
        }
    }

    private IEnumerator _Post(string url, Dictionary<string, string> arguments, Action<string> callback)
    {
        List<IMultipartFormSection> arg_data = new List<IMultipartFormSection>();

        foreach (KeyValuePair<string, string> item in arguments)
        {
            arg_data.Add(new MultipartFormDataSection(item.Key, item.Value));
        }

        UnityWebRequest www = UnityWebRequest.Post(url, arg_data);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            callback(www.error);
        }
        else
        {
            callback("It worked");
        }
    }

    private IEnumerator _Put(string url, byte[] data, Action<string> callback)
    {
        UnityWebRequest www = UnityWebRequest.Put(url, data);

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            callback(www.error);
        }
        else
        {
            callback("It worked");
        }
    }
}
