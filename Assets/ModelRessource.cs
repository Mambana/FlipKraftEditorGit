using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void addCollections(string name, string desc, string projectId)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("fk_id_project", projectId);
        string json = api.request(toAdd, "/api/ressource", "POST");
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        ModelRessource.i += 1;
    }

    public Dictionary<string,string> find(int id)
    {
        Dictionary<string, string> ressourceData = new Dictionary<string, string>();
        string json = api.request(null, "/api/ressource/" + id.ToString() + "/", "GET");
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        ressourceData.Add("name", resp["name"].ToString());
        ressourceData.Add("description", resp["description"].ToString());
        ressourceData.Add("fk_id_project", resp["fk_id_project"].ToString());
        return (ressourceData);
    }


    public int getNbElement()
    {
        return (ModelRessource.i);
    }

    public Dictionary<int, Dictionary<string, string>> getAll(string id)
    {
        Dictionary<int, Dictionary<string, string>> allRessource = new Dictionary<int, Dictionary<string, string>>();
        string json = api.request(null, "/api/ressource" + "?id=" + id, "GET");

        List<object> respList = DeserializeJson<List<object>>(json);
        int i = 0;

        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> ressourceData = new Dictionary<string, string>();

            ressourceData.Add("name", resp["name"].ToString());
            ressourceData.Add("description", resp["description"].ToString());
            ressourceData.Add("id", resp["id"].ToString());
            ressourceData.Add("fk_id_project", resp["fk_id_project"].ToString());
            allRessource.Add(i, ressourceData);
            i++;
        }

        return (allRessource);
    }

    public void updateField(string id, string projectId, string name, string desc)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("fk_id_project", projectId);
        string json = api.request(toAdd, "/api/ressource/" + id + "/", "PUT");
    }

    public void removeElem(int id)
    {
        api.request(null, "/api/ressource/" + id.ToString() + "/", "DELETE");
    }
}
