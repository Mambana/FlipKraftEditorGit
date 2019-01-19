using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAssociation : MonoBehaviour
{
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

    public void addCollections(string value, string projectId, string cardId, string ressourceId, string posX, string posY)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("value", value);
        toAdd.Add("fk_id_project", projectId);
        toAdd.Add("fk_id_cards", cardId);
        toAdd.Add("fk_id_ressource", ressourceId);
        toAdd.Add("posX", posX);
        toAdd.Add("posY", posY);
        string json = api.request(toAdd, "/api/card/association", "POST");
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        print(resp["id"].ToString());
        ModelAssociation.i = int.Parse(resp["id"].ToString());

    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public Dictionary<string, string> find(int id)
    {
        Dictionary<string, string> assocData = new Dictionary<string, string>();
        string json = api.request(null, "/api/card/association/" + id.ToString() + "/", "GET");
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        assocData.Add("name", resp["name"].ToString());
        assocData.Add("description", resp["description"].ToString());
        assocData.Add("fk_id_project", resp["fk_id_project"].ToString());
        return (assocData);
    }


    public int getNbElement()
    {
        return (ModelAssociation.i);
    }

    public Dictionary<int, Dictionary<string, string>> getAll(string id)
    {
        Dictionary<int, Dictionary<string, string>> allCard = new Dictionary<int, Dictionary<string, string>>();
        string json = api.request(null, "/api/card" + "?id=" + id, "GET");

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

    public void updateField(string assocId, string value, string projectId, string cardId, string ressourceId, string posX, string posY)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("value", value);
        toAdd.Add("fk_id_project", projectId);
        toAdd.Add("fk_id_cards", cardId);
        toAdd.Add("fk_id_ressource", ressourceId);
        toAdd.Add("posX", posX);
        toAdd.Add("posY", posY);
        string json = api.request(toAdd, "/api/card/association/"+assocId.ToString(), "PUT");
    }

    public void removeElem(int id)
    {
        api.request(null, "/api/card/association/" + id.ToString() , "DELETE");
    }
}
