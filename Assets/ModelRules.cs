using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

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
        api.request(toAdd, "/api/project/"+ projectId+"/pack", "POST", callback);

    }

    public void find(int id, Action<string> callback)
    {
        api.request(null, "/api/rule/" + id.ToString() + "/", "GET", callback);
    }


    public int getNbElement()
    {
        return (ModelRules.i);
    }

    public void getAll(string id, Action<string> callback)
    {
        api.request(null, "/api/rule" + "?id=" + id, "GET", callback);

    }

    public void updateField(string id, string projectId, string name, string desc, string priority, Action<string> callback = null)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("fk_id_project", projectId);
        toAdd.Add("priority", priority);
        toAdd.Add("playable", "true");
        api.request(toAdd, "/api/phase/" + id + "/", "PUT", callback);
    }

    public void removeElem(int id, Action<string> callback)
    {
        api.request(null, "/api/phase/" + id.ToString() + "/", "DELETE", callback);
    }
}
