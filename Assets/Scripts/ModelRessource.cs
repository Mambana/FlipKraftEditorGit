using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModelRessource : MonoBehaviour {
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

    public void addCollections(string name, string desc, string projectId, Action<string> callback)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("fk_id_project", projectId);
        api.request(toAdd, "/api/ressource", "POST", callback);
        
    }

    public void find(int id, Action<string> callback)
    {
      api.request(null, "/api/ressource/" + id.ToString() + "/", "GET", callback);  
    }


    public int getNbElement()
    {
        return (ModelRessource.i);
    }

    public void getAll(string id, Action<string>callback)
    {
         api.request(null, "/api/ressource" + "?id=" + id, "GET",callback);
        
    }

    public void updateField(string id, string projectId, string name, string desc, Action<string> callback = null)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("fk_id_project", projectId);
        api.request(toAdd, "/api/ressource/" + id + "/", "PUT", callback);
    }

    public void removeElem(int id)
    {
        api.request(null, "/api/ressource/" + id.ToString() + "/", "DELETE", null);
    }
}
