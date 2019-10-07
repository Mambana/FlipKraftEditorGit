using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Linq;

public class ModelRules : MonoBehaviour
{
    apiConnection api;
    static int i = 0;
    // Use this for initialization
    void Start()
    {
        api = GameObject.Find("api_connection").GetComponent<apiConnection>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void addCollections(string projectName, string name, string desc, string[] signal,
        string[] var_type, string[] instruction, string[] variables, string[] var_description,
        string priority,
        Action<string> callback)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();
        print(JsonConvert.SerializeObject(signal, Formatting.Indented));
        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("signals", JsonConvert.SerializeObject(signal, Formatting.Indented));
        toAdd.Add("var_type", JsonConvert.SerializeObject(var_type, Formatting.Indented));
        toAdd.Add("instructions", JsonConvert.SerializeObject(instruction, Formatting.Indented));
        toAdd.Add("variables", JsonConvert.SerializeObject(variables, Formatting.Indented));
        toAdd.Add("var_description", JsonConvert.SerializeObject(var_description, Formatting.Indented));
        toAdd.Add("priority", priority);
        api.request(toAdd, "/api/project/"+ projectName+"/pack", "POST", callback);

    }

    public void find(int id, string projectName,  Action<string> callback)
    {
        api.request(null, "/api/project/" +projectName+ "/pack/"+ id.ToString() + "/", "GET", callback);
    }


    public int getNbElement()
    {
        return (ModelRules.i);
    }

    public void getAll(string projectName, Action<string> callback)
    {
        api.request(null, "/api/project/" + projectName + "/pack", "GET", callback);

    }

    public string epurJson(string json)
    {
        print(json);
        string epur = json.Replace("\\", "");
        epur = epur.Substring(1, epur.Length - 2);
        //print(epur);
        return (epur);
    }

    public void updateField(string id, string projectName, string name, string desc, string[] signal,
        string[] var_type, string[] instruction, string[] variables, string[] var_description,
        string priority,
        Action<string> callback)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();
        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("signals",  JsonConvert.SerializeObject(signal.Select(x => x.Replace("\r\n", "")).ToArray(), Formatting.None) );
        toAdd.Add("var_type", JsonConvert.SerializeObject(var_type.Select(x => x.Replace("\r\n", "")).ToArray(), Formatting.None));
        toAdd.Add("instructions", JsonConvert.SerializeObject(instruction.Select(x => x.Replace("\r\n", "")).ToArray(), Formatting.None));
        toAdd.Add("variables", JsonConvert.SerializeObject(variables.Select(x => x.Replace("\r\n", "")).ToArray(), Formatting.None));
        toAdd.Add("var_description", JsonConvert.SerializeObject(var_description, Formatting.None));
        toAdd.Add("priority", priority);
        api.request(toAdd, "/api/project/" + projectName + "/pack/" + id + "/", "PUT", callback, null, null, true);
    }

    public void removeElem(int id, string projectName, Action<string> callback)
    {
        api.request(null, "/api/project/"+ projectName +"/pack/" + id.ToString() + "/", "DELETE", callback);
    }
}
