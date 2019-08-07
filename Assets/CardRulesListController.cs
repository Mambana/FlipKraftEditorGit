using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardRulesListController : BasicController
{
    int projectId;
    int cardId;
    GameObject model;
    [SerializeField]
    GameObject elemInList;
    [SerializeField]
    GameObject listOfRules;
    [SerializeField]
    GameObject cancelButton;

    [SerializeField]
    List<GameObject> buttonList;

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
        
        Dictionary<int, Dictionary<string, string>> allProj = new Dictionary<int, Dictionary<string, string>>();
        List<object> respList = DeserializeJson<List<object>>(json);
        int i = 0;
        foreach (GameObject button in buttonList)
        {
            Destroy(button);
        }
        buttonList.Clear();
        allProj.Clear();
        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> projectData = new Dictionary<string, string>();

            projectData.Add("name", resp["name"].ToString());
            projectData.Add("description", resp["description"].ToString());
            projectData.Add("id", resp["id"].ToString());
            allProj.Add(i, projectData);
            i++;
        }

        foreach (KeyValuePair<int, Dictionary<string, string>> project in allProj)
        {
            GameObject toAdd = Instantiate(elemInList) as GameObject;
            toAdd.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = project.Value["name"];
            toAdd.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = project.Value["description"];
            toAdd.GetComponent<ModifyCardRulesButton>().setRuleId(int.Parse(project.Value["id"]));//
            toAdd.GetComponent<ModifyCardRulesButton>().setProjectId(projectId);
            toAdd.GetComponent<ModifyCardRulesButton>().setCardId(cardId);
            buttonList.Add(toAdd);
            if (listOfRules)
                toAdd.transform.SetParent(listOfRules.transform, false);

        }
    }

    override public void apply()
    {
        projectId = int.Parse(args["project_id"]);
        cardId = int.Parse(args["card_id"]);
        model = GameObject.Find("ModelCardRules");
        ModelCardRules modelScript = model.GetComponent<ModelCardRules>();
        modelScript.getAll(cardId.ToString(), applyInServerResponse);
        cancelButton.GetComponent<CancelCardRulesListCreation>().setCardId(cardId.ToString());
        cancelButton.GetComponent<CancelCardRulesListCreation>().setProjectId(projectId.ToString());


    }
}
