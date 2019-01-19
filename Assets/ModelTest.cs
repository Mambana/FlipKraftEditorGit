using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;



public class ModelTest : MonoBehaviour {
    Dictionary<int, Dictionary<string, string>> model;
    apiConnection api;
    static int i = 0;
	// Use this for initialization
	void Start () {
        model = new Dictionary<int, Dictionary<string, string>>();
        api = GameObject.Find("api_connection").GetComponent<apiConnection>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void addCollections(string name, string min, string max,
        string desc, string nb_card, string nb_re)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("async_game", "0");
        toAdd.Add("turn_game", "1");
        toAdd.Add("min_player", min);
        toAdd.Add("max_player", max);
        toAdd.Add("description", desc);
        string json = api.request(toAdd, "/api/project", "POST");
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        ModelTest.i = int.Parse(resp["id"].ToString()) + 1;
    }

    public Dictionary<string,string> find(int id)
    {
        Dictionary<string, string> param = new Dictionary<string, string>();
        Dictionary<string, string> projectData = new Dictionary<string, string>();
        string json = api.request(param, "/api/project/" + id.ToString() + "/", "GET");
        
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        projectData.Add("name", resp["name"].ToString());
        projectData.Add("async_game", resp["async_game"].ToString());
        projectData.Add("turn_game", resp["turn_game"].ToString());
        projectData.Add("min_player", resp["min_player"].ToString());
        projectData.Add("max_player", resp["max_player"].ToString());
        projectData.Add("description", resp["description"].ToString());

        return (projectData);
    }


    public int getNbElement()
    {
        return (ModelTest.i);
    }

    public Dictionary<int, Dictionary<string, string>> getAll()
    {
        Dictionary<int, Dictionary<string, string>> allProj = new Dictionary<int, Dictionary<string, string>>();
        string json = api.request(null, "/api/project", "GET");
       
       List<object> respList =  DeserializeJson<List<object>>(json);
        int i = 0;
        
        foreach (object obj in respList)
        {
            print(obj.ToString());
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> projectData = new Dictionary<string, string>();

            projectData.Add("name", resp["name"].ToString());
            projectData.Add("async_game", resp["async_game"].ToString());
            projectData.Add("turn_game", resp["turn_game"].ToString());
            projectData.Add("min_player", resp["min_player"].ToString());
            projectData.Add("max_player", resp["max_player"].ToString());
            projectData.Add("description", resp["description"].ToString());
            projectData.Add("id", resp["id"].ToString());
            allProj.Add(i, projectData);
            i++;
        }
        
        return (allProj);
    }

    public void updateField(string id, string name, string min, string max,
        string desc)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("async_game", "0");
        toAdd.Add("turn_game", "1");
        toAdd.Add("min_player", min);
        toAdd.Add("max_player", max);
        toAdd.Add("description", desc);
        string json = api.request(toAdd, "/api/project/" + id+ "/", "PUT");
       
    }

    public void removeElem(int id)
    {

        api.request(null, "/api/project/" +id.ToString()+"/", "DELETE");
    }
}
