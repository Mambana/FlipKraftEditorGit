using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
public class ModelRessource : MonoBehaviour {
    //TEST FOR UNITY GIT//
 
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

    public void updateField(string id, string projectId, string name, string desc)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("fk_id_project", projectId);
        api.request(toAdd, "/api/ressource/" + id + "/", "PUT", null);
    }

    public void removeElem(int id)
    {
        api.request(null, "/api/ressource/" + id.ToString() + "/", "DELETE", null);
    }
}
