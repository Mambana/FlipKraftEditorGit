using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModelCard : MonoBehaviour {
    
    Dictionary<int, Dictionary<string, string>> model;
    apiConnection api;
    static int i = 0;
    // Use this for initialization
    void Start()
    {
        model = new Dictionary<int, Dictionary<string, string>>();
        api = GameObject.Find("api_connection").GetComponent<apiConnection>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addCollections(string name, string desc, string projectId, string projectName, Action<string> callback, string phasesId = "0")
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("fk_id_project", projectId);
        toAdd.Add("fk_id_phase", phasesId);
        api.request(toAdd, "/api/project/" + projectName +"/card", "POST", callback);
    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void find(int id, string projectName, Action<string> callback)
    {   
        api.request(null, "/api/project/" + projectName + "/card/" + id.ToString() + "/", "GET", callback);
    }


    public int getNbElement()
    {
        return (ModelCard.i);
    }

    public void getAll(string projectName, Action<string> callback)
    {
       api.request(null, "/api/project/" + projectName + "/card", "GET", callback); 
    }

    public void updateField(string id, string name, string desc, string projectId, string projectName, Action<string> callback = null)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("fk_id_project", projectId);
        api.request(toAdd, "/api/project/" + projectName +"/card/" + id + "/", "PUT", callback);
    }

    public void removeElem(int id, string projectName, Action<string> callback = null)
    {/// api / project /{ name}/ card /{ id}/
               api.request(null, "/api/project/" + projectName + "/card/" + id.ToString() + "/", "DELETE", callback);
      
    }

 
}
