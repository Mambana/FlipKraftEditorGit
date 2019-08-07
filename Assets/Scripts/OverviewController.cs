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
    [SerializeField]
    GameObject modelPhases;
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
    GameObject createPhaseBut;
    [SerializeField]
    GameObject createRuleBut;
    [SerializeField]
    GameObject rulesListBut;
    [SerializeField]
    GameObject newResBut;
    [SerializeField]
    GameObject elemInList;
    [SerializeField]
    GameObject updateProj;
    [SerializeField]
    GameObject listOfRess;
    [SerializeField]
    GameObject phaseItemContainer;
    [SerializeField]
    GameObject phaseItem;

    ImageHandler imgHandler;
    DeckListHandler cardListScr;
    
    List<GameObject> ressources;
    List<GameObject> phases;
    [SerializeField]
    GameObject asyncToggle;
    [SerializeField]
    GameObject turnToggle;
    // Use this for initialization
    void Start () {
        ressources = new List<GameObject>();
        phases = new List<GameObject>();
        imgHandler = GameObject.Find("ImageHandler").GetComponent<ImageHandler>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    private void destroyChilds(GameObject parent)
    {
        foreach(Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void destroyList(List<GameObject> l)
    {
        foreach (GameObject obj in l)
        {
            Destroy(obj.gameObject);
        }
        l.Clear();
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
        projName.GetComponent<TMP_InputField>().text = " " + projectData["name"];
        min.GetComponent<TMP_InputField>().text = " " + projectData["min_player"];
        max.GetComponent<TMP_InputField>().text = " " + projectData["max_player"];
        desc.GetComponent<TMP_InputField>().text = " " + projectData["description"];

        if (projectData["async_game"].Equals("True"))
            asyncToggle.GetComponent<Toggle>().isOn = true;
        else
            asyncToggle.GetComponent<Toggle>().isOn = false;
        if (projectData["turn_game"].Equals("True"))
            turnToggle.GetComponent<Toggle>().isOn = true;
        else
            turnToggle.GetComponent<Toggle>().isOn = false;
    }

    public void applyForCards(string json)
    {
        Dictionary<int, Dictionary<string, string>> allCard = new Dictionary<int, Dictionary<string, string>>();
        List<object> respList = DeserializeJson<List<object>>(json);
        int i = 0;
        cardListScr = cardList.GetComponent<DeckListHandler>();
        cardListScr.RemoveAllDeck();
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
        imgHandler = GameObject.Find("ImageHandler").GetComponent<ImageHandler>();
        int i = 0;
        destroyChilds(listOfRess);
     //   destroyList(ressources);
        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> ressourceData = new Dictionary<string, string>();

            ressourceData.Add("name", resp["name"].ToString());
            ressourceData.Add("description", resp["description"].ToString());
            ressourceData.Add("id", resp["id"].ToString());
            ressourceData.Add("fk_id_project", resp["fk_id_project"].ToString());
            ressourceData.Add("img_id", resp["img_id"].ToString());
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
            toAdd.GetComponent<Image>().sprite = imgHandler.GetSprite(int.Parse(project.Value["img_id"]));
         //   ressources.Add(toAdd);
        }

    }

    public void applyForPhases(string json)
    {
        Dictionary<int, Dictionary<string, string>> allPhases = new Dictionary<int, Dictionary<string, string>>();
        List<object> respList = DeserializeJson<List<object>>(json);
        int i = 0;
        destroyChilds(phaseItemContainer);
   //     destroyList(phases);
        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> phaseData = new Dictionary<string, string>();

            phaseData.Add("name", resp["name"].ToString());
            phaseData.Add("description", resp["description"].ToString());
            phaseData.Add("priority", resp["priority"].ToString());
            phaseData.Add("id", resp["id"].ToString());
            phaseData.Add("fk_id_project", resp["fk_id_project"].ToString());
            allPhases.Add(i, phaseData);
            i++;
        }
        foreach (KeyValuePair<int, Dictionary<string, string>> project in allPhases)
        {
            GameObject toAdd = Instantiate(phaseItem) as GameObject;

            toAdd.transform.Find("PhaseName").GetComponent<TextMeshProUGUI>().text = project.Value["name"];
            toAdd.transform.Find("Index").GetComponent<TextMeshProUGUI>().text = project.Value["priority"];
            ModifyRessourceButton toAddScr = toAdd.transform.Find("EditButton").GetComponent<ModifyRessourceButton>();
            RemovePhaseButton rmScr = toAdd.transform.Find("RemoveButton").GetComponent<RemovePhaseButton>();
            toAddScr.setIdToModify(project.Value["id"]);
            toAddScr.setProjectId(project.Value["fk_id_project"]);
            rmScr.setIdToRemove(int.Parse(project.Value["id"]));
            rmScr.setProjectId(project.Value["fk_id_project"]);
            toAdd.transform.SetParent(phaseItemContainer.transform, false);
          //  phases.Add(toAdd);
        }
    }

    public override void apply()
    {


        Model = GameObject.Find("Model");

        ModelRessources = GameObject.Find("ModelRessource");

        modelPhases = GameObject.Find("ModelPhases");
        ModelCards = GameObject.Find("ModelCard");

        foreach (KeyValuePair< string, string> arg in args)
         
        id = int.Parse(args["id"]);      
        Model = GameObject.Find("Model");
        newResBut.GetComponent<CreateRessourceButton>().setProjectId(id);
        createPhaseBut.GetComponent<CreateRessourceButton>().setProjectId(id);
        updateProj.GetComponent<ConfirmModifyButton>().setIdToModify(id);
        createCardBut.GetComponent<CreateCardButton>().setProjectId(id);
        createRuleBut.GetComponent<CreateCardButton>().setProjectId(id);
        rulesListBut.GetComponent<RessourceListButton>().setCurrentProjectId(id);
        ModelTest ModelScript = Model.GetComponent<ModelTest>();
        ModelScript.find(id, applyInServerResponse);
        ModelCard modelCardScript = ModelCards.GetComponent<ModelCard>();
        modelCardScript.getAll(id.ToString(), applyForCards);
        ModelRessource ModelRessourceScr = ModelRessources.GetComponent<ModelRessource>();
        ModelRessourceScr.getAll(id.ToString(), applyForRessource);
        ModelPhases modelPhasesScr = modelPhases.GetComponent<ModelPhases>();
        modelPhasesScr.getAll(id.ToString(), applyForPhases);
    }

    
}
