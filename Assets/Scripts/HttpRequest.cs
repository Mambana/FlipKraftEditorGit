using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class HttpRequest : MonoBehaviour
{
    private GameObject session;
    private SessionData data;
    // Start is called before the first frame update
    void Start()
    {
        session = GameObject.Find("Session");
        data = session.GetComponent<SessionData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Get(string url, Dictionary<string, string> arguments, Action<string> callback)
    {
        url += "?";

        foreach (KeyValuePair<string, string> item in arguments)
        {
            url += item.Key + "=" + item.Value + "&";
        }
        StartCoroutine(_Get(url, callback));
    }

    public void Get(string url, Action<string> callback)
    {
        StartCoroutine(_Get(url, callback));
    }

    public void Post(string url, Dictionary<string, string> arguments, Action<string> callback)
    {
        StartCoroutine(_Post(url, arguments, callback));
    }

    public void Delete(string url, Action<string> callback)
    {
        StartCoroutine(_Delete(url, callback));
    }

    public void Put(string url, byte[] data, Action<string> callback)
    {
        StartCoroutine(_Put(url, data, callback));
    }

    private IEnumerator _Get(string url, Action<string> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);

        AddLogInformation(ref www);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
            callback(www.error);
        else
            callback(www.downloadHandler.text);
    }

    private IEnumerator _Delete(string url, Action<string> callback)
    {
        UnityWebRequest www = UnityWebRequest.Delete(url);

        AddLogInformation(ref www);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
            callback(www.error);
        else
            callback(www.downloadHandler.text);
    }

    private IEnumerator _Post(string url, Dictionary<string, string> arguments, Action<string> callback)
    {
        List<IMultipartFormSection> arg_data = new List<IMultipartFormSection>();

        foreach (KeyValuePair<string, string> item in arguments)
        {
            arg_data.Add(new MultipartFormDataSection(item.Key, item.Value));
        }
        UnityWebRequest www = UnityWebRequest.Post(url, arg_data);
        AddLogInformation(ref www);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            callback(www.error);
        else
            callback("It worked");
    }

    private IEnumerator _Put(string url, byte[] data, Action<string> callback)
    {
        UnityWebRequest www = UnityWebRequest.Put(url, data);

        AddLogInformation(ref www);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
            callback(www.error);
        else
            callback("It worked");
    }

    private void AddLogInformation(ref UnityWebRequest www)
    {
        string login = data.access("email");
        string pwd = data.access("pwd");

        print("email : " + login + ", pwd : " + pwd);
        www.SetRequestHeader("login", login);
        www.SetRequestHeader("password", pwd);
    }
}
