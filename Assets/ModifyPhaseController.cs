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
    int projectId;
    GameObject model;
    int idToModify;
    // Start is called before the first frame update
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
        Dictionary<string, string> phaseData = new Dictionary<string, string>();
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        phaseData.Add("name", resp["name"].ToString());
        phaseData.Add("description", resp["description"].ToString());
        phaseData.Add("fk_id_project", resp["fk_id_project"].ToString());
        phaseData.Add("priority", resp["priority"].ToString());
        inputName.GetComponent<TMP_InputField>().text = phaseData["name"];
        inputDesc.GetComponent<TMP_InputField>().text = phaseData["description"];
        inputPriority.GetComponent<TMP_InputField>().text = phaseData["priority"];
    }

    public override void apply()
    {
        int id = int.Parse(args["id"]);

        model = GameObject.Find("ModelPhases");
        ModelPhases modelScr = model.GetComponent<ModelPhases>();
        modelScr.find(id, applyInServerResponse);
        ConfirmModifyPhaseButton butScr = confirmButton.GetComponent<ConfirmModifyPhaseButton>();
        butScr.setProjectId(args["project_id"]);
        butScr.setIdToModify(id);
       RemovePhaseButton removeBut = removeButton.GetComponent<RemovePhaseButton>();
        removeBut.setProjectId(args["project_id"]);
        removeBut.setIdToRemove(id);
        
    }
}
