using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Experimental.Networking;

public class cardRules
{
   
    public string name;
    public string description;
    public string[] signals;
    public string[] var_type;
    public string[] instructions;
    public string[] variables;
    public string[] var_description;
    public string priority;
}

public class apiConnection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject errorPopup;
    [SerializeField]
    private GameObject canvas;
    SessionData scrData;
    void Start()
    {
        scrData = GameObject.Find("Session").GetComponent<SessionData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    IEnumerator getRequest(string route, Action<string> call, Action<string, GameObject> callOnObject = null, GameObject obj = null)
    {
        string authorization = authenticate(scrData.access("email"), scrData.access("pwd"));
        string url = scrData.access("api_address") + route;

        print(url);
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {

            www.SetRequestHeader("AUTHORIZATION", authorization);

            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
               if (call != null)
                    call("none");
                else
                    instantiateErrorPopup();
                print(www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                if (call != null)
                 call(json);
                if (callOnObject != null)
                    callOnObject(json, obj);
            }
        }
    }

    IEnumerator postRequest(string route, Dictionary<string, string> fields, Action<string> call)
    {
        string authorization = authenticate(scrData.access("email"), scrData.access("pwd"));
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> f in fields)
            form.AddField(f.Key, f.Value);
       
        using (UnityWebRequest www = UnityWebRequest.Post(scrData.access("api_address") + route, form))
        {
          
            www.SetRequestHeader("AUTHORIZATION", authorization);

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                print(www.downloadHandler.text);
                print(www.error);
                instantiateErrorPopup();


            }
            else
            {
                print(www.downloadHandler.text);
                if (call != null)
                    call(www.downloadHandler.text);
            }
        }
    }

    string[] parseJsonArray(string json)
    {
        List<object> objs = DeserializeJson<List<object>>(json);
        List<string> strs = new List<string>();
        foreach(object ob in objs)
        {
            strs.Add(ob.ToString());
        }

        return (strs.ToArray());
    }

    string parseDictionary(Dictionary<string, string> dic)
    {
        cardRules rule = new cardRules();
        rule.name = dic["name"];
        rule.description = dic["description"];
        print(rule.description);
        rule.signals = parseJsonArray(dic["signals"]);
        rule.variables = parseJsonArray(dic["variables"]);
        rule.var_type = parseJsonArray(dic["var_type"]);
        rule.instructions = parseJsonArray(dic["instructions"]);
        rule.var_description = parseJsonArray(dic["var_description"]);
        rule.priority = dic["priority"];
        return (JsonConvert.SerializeObject(rule, Formatting.Indented));
    }

    IEnumerator putRequest(string route, Dictionary<string, string> fields, Action<string> call, bool askParse = false)
    {
        string authorization = authenticate(scrData.access("email"), scrData.access("pwd"));
        string json =  JsonConvert.SerializeObject(fields, Formatting.Indented);

        print(json);
        /*if (askParse)
        {
            json = parseDictionary(fields);
        }*/
        print(route);
        using (UnityWebRequest www = UnityWebRequest.Put(scrData.access("api_address") + route, json))
        {
            www.SetRequestHeader("AUTHORIZATION", authorization);
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "application/json");

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                print(www.error);
                print(scrData.access("api_address") + route);
           
                instantiateErrorPopup();
            }
            else
            {
                print(www.downloadHandler.text);
                if (call != null)
                    call(www.downloadHandler.text);
            }
        }
    }

    IEnumerator deleteRequest(string route, Action<string> call = null)
    {
        string authorization = authenticate(scrData.access("email"), scrData.access("pwd"));
        using (UnityWebRequest www = UnityWebRequest.Delete(scrData.access("api_address") + route))
        {
            www.SetRequestHeader("AUTHORIZATION", authorization);
            print("delete/" + route);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                instantiateErrorPopup();
                print(www.error);
            }
            else
            {
              //  print(www.downloadHandler.text);
                if (call != null)
                    call("");
            }

        }
    }

    string authenticate(string username, string password)
    {
        string auth = username + ":" + password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }

    public void request(Dictionary<string, string> toAdd, string route, string method,
        Action<string> call = null, Action<string, GameObject> callOnObject = null, GameObject obj = null, bool askParse = false)
    {
        
        if (method.Equals("GET"))
            StartCoroutine(getRequest(route, call, callOnObject, obj));
        if (method.Equals("POST"))
            StartCoroutine(postRequest(route, toAdd,call));
        if (method.Equals("PUT"))
            StartCoroutine(putRequest(route, toAdd, call, askParse));
        if (method.Equals("DELETE"))
            StartCoroutine(deleteRequest(route,call));
     
    }
   
    public void instantiateErrorPopup()
    {
        GameObject element = Instantiate(errorPopup) as GameObject;
        element.transform.SetParent(canvas.transform, false);
    }

    public string request(Dictionary<string, string> toAdd, string route, string method)
    {
      var request = (HttpWebRequest)WebRequest.Create(scrData.access("api_address") + route);
     string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(scrData.access("email") + ":" + scrData.access("pwd")));
     request.Headers.Add("Authorization", "Basic " + svcCredentials);
     request.ContentType = "application/json";
     request.Method = method;
     print(scrData.access("api_address") + route + " "+method);
     if (method.Equals("POST") || method.Equals("PUT"))
     {
         using (var streamWriter = new StreamWriter(request.GetRequestStream()))
         {
             string json = JsonConvert.SerializeObject(toAdd);
             streamWriter.Write(json);
             print(json);
             streamWriter.Flush();
             streamWriter.Close();
         }
     }
     string result;
     var httpResponse = (HttpWebResponse)request.GetResponse();
      using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
      {
          result = streamReader.ReadToEnd();
         print(result);

     }
     httpResponse.Close();

     return (result);
    }
}
