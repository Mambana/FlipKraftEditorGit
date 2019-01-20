using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCard : MonoBehaviour {
    
    Dictionary<int, Dictionary<string, string>> model;
    apiConnection api;
    static int i = 0;
    List<object> lastAssoc;
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

    public void addCollections(string name, string desc, string projectId)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("fk_id_project", projectId);
        string json = api.request(toAdd, "/api/card", "POST");
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        print(resp["id"].ToString());
        ModelCard.i = int.Parse(resp["id"].ToString());
       
    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public Dictionary<string, string> find(int id)
    {
        
        Dictionary<string, string> cardData = new Dictionary<string, string>();
        string json = api.request(null, "/api/card/" + id.ToString() + "/", "GET");
        print(json);
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        cardData.Add("name", resp["name"].ToString());
        cardData.Add("description", resp["description"].ToString());
        cardData.Add("fk_id_project", resp["fk_id_project"].ToString());
        lastAssoc =  DeserializeJson<List<object>>(resp["associations"].ToString());
        return (cardData);
    }


    public int getNbElement()
    {
        return (ModelCard.i);
    }

    public Dictionary<int, Dictionary<string, string>> getAll(string id)
    {
        Dictionary<int, Dictionary<string, string>> allCard = new Dictionary<int, Dictionary<string, string>>();
        string json = api.request(null, "/api/card" + "?id="+id, "GET");

        List<object> respList = DeserializeJson<List<object>>(json);
        int i = 0;

        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> cardData = new Dictionary<string, string>();

            cardData.Add("name", resp["name"].ToString());
            cardData.Add("description", resp["description"].ToString());
            cardData.Add("id", resp["id"].ToString());
            cardData.Add("fk_id_project", resp["fk_id_project"].ToString());
            allCard.Add(i, cardData);
            i++;
        }

        return (allCard);
    }

    public void updateField(string id, string name, string desc, string projectId)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("fk_id_project", projectId);
        string json = api.request(toAdd, "/api/card/" + id + "/", "PUT");
    }

    public void removeElem(int id)
    {
        api.request(null, "/api/card/" + id.ToString() + "/", "DELETE");
    }

    public Dictionary<int, Dictionary<string, string>> getLastAssoc()
    {
        Dictionary<int, Dictionary<string, string>> assocList = new Dictionary<int, Dictionary<string, string>>();
        int i = 0;
        foreach (object obj in lastAssoc)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> assoc = new Dictionary<string, string>();
            assoc.Add("id", resp["id"].ToString());
            assoc.Add("fk_id_ressource", resp["fk_id_ressource"].ToString());
            assoc.Add("fk_id_cards", resp["fk_id_cards"].ToString());
            assoc.Add("fk_id_project", resp["fk_id_project"].ToString());
            assoc.Add("value", resp["value"].ToString());
            assoc.Add("posX", resp["posX"].ToString());
            assoc.Add("posY", resp["posY"].ToString());
            assocList.Add(i, assoc);
            i++;
        }
        return (assocList);
    }
}
