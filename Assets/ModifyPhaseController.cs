using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;


public class ModifyPhaseController : BasicController
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputDesc;
    [SerializeField]
    GameObject inputPriority;
    [SerializeField]
    GameObject confirmButton;
    [SerializeField]
    GameObject removeButton;
    [SerializeField]
    GameObject LastRulesTxt;
    [SerializeField]
    GameObject btnRmPack;
    int projectId;
    GameObject model;
    int idToModify;
    string projectName;
    List<pack> packList;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void popLastRule()
    {
        if (packList.Count > 0)
        {
            packList.RemoveAt(packList.Count - 1);
            if (packList.Count != 0)
                LastRulesTxt.GetComponent<TextMeshProUGUI>().text = packList[packList.Count - 1].name;
        }
        else 
            LastRulesTxt.GetComponent<TextMeshProUGUI>().text = "no more rules ...";
    }

    public List<pack> getPackList()
    {
        return (packList);
    }
    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void applyInServerResponse(string json)
    {
        Dictionary<string, string> phaseData = new Dictionary<string, string>();
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        phaseData.Add("name", resp["name"].ToString());
        phaseData.Add("description", resp["description"].ToString());
        phaseData.Add("fk_id_project", resp["fk_id_project"].ToString());
        phaseData.Add("priority", resp["priority"].ToString());
        if (resp.ContainsKey("pack"))
        {
            packList = DeserializeJson<List<pack>>(resp["pack"].ToString());
            if (packList.Count > 0)
                LastRulesTxt.GetComponent<TextMeshProUGUI>().text = packList[packList.Count - 1].name;

        }
        inputName.GetComponent<TMP_InputField>().text = phaseData["name"];
        inputDesc.GetComponent<TMP_InputField>().text = phaseData["description"];
        inputPriority.GetComponent<TMP_InputField>().text = phaseData["priority"];
    }

    public override void apply()
    {
        int id = int.Parse(args["id"]);
        projectName = args["project_name"];
        model = GameObject.Find("ModelPhases");
        ModelPhases modelScr = model.GetComponent<ModelPhases>();
        modelScr.find(id, projectName, applyInServerResponse);
        ConfirmModifyPhaseButton butScr = confirmButton.GetComponent<ConfirmModifyPhaseButton>();
        butScr.setProjectId(args["project_id"]);
        butScr.setIdToModify(id);
        butScr.setProjectName(projectName);
       RemovePhaseButton removeBut = removeButton.GetComponent<RemovePhaseButton>();
        removeBut.setProjectId(args["project_id"]);
        removeBut.setIdToRemove(id);
        removeBut.setProjectName(projectName);
        
    }
}
