using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

public class RulesListController : BasicController
{
    int projectId;
    string projectName;
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

  

    override public void apply()
    {
        projectId = int.Parse(args["project_id"]);
        projectName = args["project_name"];
    
        cancelButton.GetComponent<CancelRessourceCreation>().setProjectId(projectId.ToString());
        cancelButton.GetComponent<CancelRessourceCreation>().setRessourceId(projectId.ToString());
        cancelButton.GetComponent<CancelRessourceCreation>().setProjectName(projectName);

        foreach(GameObject btn in buttonList)
        {
            btn.GetComponent<CreateCardButton>().setProjectId(projectId);
            btn.GetComponent<CreateCardButton>().setProjectName(projectName);
        }
    }
}
