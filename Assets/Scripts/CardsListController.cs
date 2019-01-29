using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class CardsListController : BasicController {

    int projectId;

    [SerializeField]
    GameObject overviewButton;

    [SerializeField]
    GameObject createCardButton;

    [SerializeField]
    GameObject elemInList;

    [SerializeField]
    GameObject listOfProj;

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void applyInServerResponse(string json)
    {
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
            GameObject toAdd = Instantiate(elemInList) as GameObject;
            print(project.Value["name"]);
            print(project.Value["description"]);
            toAdd.transform.Find("CardName").GetComponent<TextMeshProUGUI>().text = project.Value["name"];
            toAdd.transform.Find("CardDesc").GetComponent<TextMeshProUGUI>().text = project.Value["description"];
            ModifyCardButton toAddScr = toAdd.GetComponent<ModifyCardButton>();
            toAddScr.setIdToModify(project.Value["id"]);
            toAddScr.setProjectId(project.Value["fk_id_project"]);
            toAdd.transform.SetParent(listOfProj.transform, false);
        }
    }

    public override void apply()
    {
        GameObject model;

        projectId = int.Parse(args["project_id"]);
        model = GameObject.Find("ModelCard");
        ModelCard modelScript = model.GetComponent<ModelCard>();
        modelScript.getAll(projectId.ToString(), applyInServerResponse);
     
        overviewButton.GetComponent<OverviewButton>().setCurrentProjectId(projectId);
        createCardButton.GetComponent<CreateCardButton>().setProjectId(projectId);

    }
}
