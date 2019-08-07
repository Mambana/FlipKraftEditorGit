using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelCardRules : MonoBehaviour
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

    public void addCollections(string projectId, string name, string desc, string[] signal,
        string[] var_type, string[] instruction, string[] variables, string[] var_description,
        string priority,
        Action<string> callback)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();
        print(projectId);
        print(JsonConvert.SerializeObject(signal, Formatting.Indented));
        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("signals", JsonConvert.SerializeObject(signal, Formatting.Indented));
        toAdd.Add("var_type", JsonConvert.SerializeObject(var_type, Formatting.Indented));
        toAdd.Add("instructions", JsonConvert.SerializeObject(instruction, Formatting.Indented));
        toAdd.Add("variables", JsonConvert.SerializeObject(variables, Formatting.Indented));
        toAdd.Add("var_description", JsonConvert.SerializeObject(var_description, Formatting.Indented));
        toAdd.Add("priority", priority);
        api.request(toAdd, "/api/card/" + projectId + "/pack", "POST", callback);

    }

    public void find(int id, int projectId, Action<string> callback)
    {
        api.request(null, "/api/card/" + projectId.ToString() + "/pack/" + id.ToString() + "/", "GET", callback);
    }


    public int getNbElement()
    {
        return (ModelCardRules.i);
    }

    public void getAll(string id, Action<string> callback)
    {
        api.request(null, "/api/card/" + id + "/pack", "GET", callback);

    }

    public string epurJson(string json)
    {
        print(json);
        string epur = json.Replace("\\", "");
        epur = epur.Substring(1, epur.Length - 2);
        //print(epur);
        return (epur);
    }

    public void updateField(string id, string projectId, string name, string desc, string[] signal,
        string[] var_type, string[] instruction, string[] variables, string[] var_description,
        string priority,
        Action<string> callback)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();
        print(projectId);
        toAdd.Add("name", name);
        toAdd.Add("description", desc); 
        toAdd.Add("signals", JsonConvert.SerializeObject(signal.Select(x => x.Replace("\r\n", "")).ToArray(), Formatting.None));
        toAdd.Add("var_type", JsonConvert.SerializeObject(var_type.Select(x => x.Replace("\r\n", "")).ToArray(), Formatting.None));
        toAdd.Add("instructions", JsonConvert.SerializeObject(instruction.Select(x => x.Replace("\r\n", "")).ToArray(), Formatting.None));
        toAdd.Add("variables", JsonConvert.SerializeObject(variables.Select(x => x.Replace("\r\n", "")).ToArray(), Formatting.None));
        toAdd.Add("var_description", JsonConvert.SerializeObject(var_description, Formatting.None));
        toAdd.Add("priority", priority);
        api.request(toAdd, "/api/card/" + projectId + "/pack/" + id + "/", "PUT", callback, null, null, true);
    }

    public void removeElem(int id, int projectId, Action<string> callback)
    {
        api.request(null, "/api/card/" + projectId.ToString() + "/pack/" + id.ToString() + "/", "DELETE", callback);
    }
}
