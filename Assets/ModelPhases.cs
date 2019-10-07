using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModelPhases : MonoBehaviour
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

    public void addCollections(string name, string desc, string priority, string projectId, string projectName, Action<string> callback)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("fk_id_project", projectId);
        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("playable", "true");
        toAdd.Add("priority", priority);
        api.request(toAdd, "/api/project/" + projectName + "/phase", "POST", callback);

    }

    public void find(int id, string projectName, Action<string> callback)
    {
        api.request(null, "/api/project/" + projectName + "/phase/" +  id.ToString() + "/", "GET", callback);
    }


    public int getNbElement()
    {
        return (ModelPhases.i);
    }

    public void getAll(string projectName, Action<string> callback)
    {
        api.request(null, "/api/project/" + projectName + "/phase", "GET", callback);

    }

    public void updateField(string id, string projectId, string projectName ,string name, string desc, string priority, Action<string> callback = null)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("fk_id_project", projectId);
        toAdd.Add("priority", priority);
        toAdd.Add("playable", "true");
        api.request(toAdd, "/api/project/" + projectName + "/phase/" + id + "/", "PUT", callback);
    }

    public void removeElem(int id, string projectName, Action<string> callback)
    {
        api.request(null, "/api/project/" + projectName + "/phase/" + id.ToString() + "/", "DELETE", callback);
    }
}
