using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;

public class OverviewController : BasicController
{
    [SerializeField]
    GameObject Model;
    int id;
    [SerializeField]
    GameObject projName;
    [SerializeField]
    GameObject min;
    [SerializeField]
    GameObject max;
    [SerializeField]
    GameObject desc;
    [SerializeField]
    GameObject nb_cards;
    [SerializeField]
    GameObject nb_ressources;
    [SerializeField]
    GameObject projNameTitle;
    [SerializeField]
    GameObject modifyButton;
    [SerializeField]
    GameObject ressourceButton;
    [SerializeField]
    GameObject cardButton;
    // Use this for initialization
    void Start () {
     


    }

    // Update is called once per frame
    void Update () {
		
	}

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void applyInServerResponse(string json)
    {
        Dictionary<string, string> projectData = new Dictionary<string, string>();
            //api.request(param, "/api/project/" + id.ToString() + "/", "GET");

        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        projectData.Add("name", resp["name"].ToString());
        projectData.Add("async_game", resp["async_game"].ToString());
        projectData.Add("turn_game", resp["turn_game"].ToString());
        projectData.Add("min_player", resp["min_player"].ToString());
        projectData.Add("max_player", resp["max_player"].ToString());
        projectData.Add("description", resp["description"].ToString());
      //  projNameTitle.GetComponent<TextMeshProUGUI>().text = projectData["name"];
        projName.GetComponent<TextMeshProUGUI>().text += " " + projectData["name"];
        min.GetComponent<TextMeshProUGUI>().text += " " + projectData["min_player"];
        max.GetComponent<TextMeshProUGUI>().text += " " + projectData["max_player"];
        desc.GetComponent<TextMeshProUGUI>().text += " " + projectData["description"];
    }

    public override void apply()
    {
       
        id = int.Parse(args["id"]);
        
        Model = GameObject.Find("Model");
        modifyButton.GetComponent<ModifyButton>().setIdToModify(id);
        ressourceButton.GetComponent<RessourceListButton>().setCurrentProjectId(id);
        cardButton.GetComponent<CardListButton>().setCurrentProjectId(id);
        ModelTest ModelScript = Model.GetComponent<ModelTest>();
        ModelScript.find(id, applyInServerResponse);
    }

    
}
