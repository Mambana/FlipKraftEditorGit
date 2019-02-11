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
    [SerializeField]
    GameObject ModelCards;
    [SerializeField]
    GameObject ModelRessources;
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
    [SerializeField]
    GameObject cardList;
    [SerializeField]
    GameObject createCardBut;
    [SerializeField]
    GameObject newResBut;
    [SerializeField]
    GameObject elemInList;
    [SerializeField]
    GameObject updateProj;
    [SerializeField]
    GameObject listOfRess;

    DeckListHandler cardListScr;
    // Use this for initialization
    void Start () {

        Model = GameObject.Find("Model");
       
        ModelRessources = GameObject.Find("ModelRessource");
        cardListScr = cardList.GetComponent<DeckListHandler>();
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
        projName.GetComponent<TMP_InputField>().text += " " + projectData["name"];
        min.GetComponent<TMP_InputField>().text += " " + projectData["min_player"];
        max.GetComponent<TMP_InputField>().text += " " + projectData["max_player"];
        desc.GetComponent<TMP_InputField>().text += " " + projectData["description"];
    }

    public void applyForCards(string json)
    {
        print(json);
        Dictionary<int, Dictionary<string, string>> allCard = new Dictionary<int, Dictionary<string, string>>();
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
        foreach (KeyValuePair<int, Dictionary<string, string>> project in allCard)
        {  
            cardListScr.AddDeck(id.ToString(), project.Value["id"], project.Value["name"]);
        }
    }

    public void applyForRessource(string json)
    {
        Dictionary<int, Dictionary<string, string>> allRessource = new Dictionary<int, Dictionary<string, string>>();
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
        foreach (KeyValuePair<int, Dictionary<string, string>> project in allRessource)
        {
            GameObject toAdd = Instantiate(elemInList) as GameObject;

            toAdd.transform.Find("RessourceName").GetComponent<TextMeshProUGUI>().text = project.Value["name"];
            toAdd.transform.Find("RessourceDesc").GetComponent<TextMeshProUGUI>().text = project.Value["description"];
            ModifyRessourceButton toAddScr = toAdd.GetComponent<ModifyRessourceButton>();
            toAddScr.setIdToModify(project.Value["id"]);
            toAddScr.setProjectId(project.Value["fk_id_project"]);
            toAdd.transform.SetParent(listOfRess.transform, false);
        }

    }

    public override void apply()
    {
        ModelCards = GameObject.Find("ModelCard");
        ModelRessources = GameObject.Find("ModelRessource");
        id = int.Parse(args["id"]);      
        Model = GameObject.Find("Model");
        newResBut.GetComponent<CreateRessourceButton>().setProjectId(id);
        updateProj.GetComponent<ConfirmModifyButton>().setIdToModify(id);
        createCardBut.GetComponent<CreateCardButton>().setProjectId(id);
        ModelTest ModelScript = Model.GetComponent<ModelTest>();
        ModelScript.find(id, applyInServerResponse);
        ModelCard modelCardScript = ModelCards.GetComponent<ModelCard>();
        modelCardScript.getAll(id.ToString(), applyForCards);
        ModelRessource ModelRessourceScr = ModelRessources.GetComponent<ModelRessource>();
        ModelRessourceScr.getAll(id.ToString(), applyForRessource);
    }

    
}
