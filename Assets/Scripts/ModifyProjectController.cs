using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;

public class ModifyProjectController : BasicController {

    // Use this for initialization
    GameObject model;
    [SerializeField]
    GameObject confirmButton;
    [SerializeField]
    GameObject removeButton;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputMin;
    [SerializeField]
    GameObject inputMax;
    [SerializeField]
    GameObject inputDesc;
    [SerializeField]
    GameObject projNameTitle;
   

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
        Dictionary<string, string> param = new Dictionary<string, string>();
        Dictionary<string, string> projectData = new Dictionary<string, string>();
        //api.request(param, "/api/project/" + id.ToString() + "/", "GET");

        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        projectData.Add("name", resp["name"].ToString());
        projectData.Add("async_game", resp["async_game"].ToString());
        projectData.Add("turn_game", resp["turn_game"].ToString());
        projectData.Add("min_player", resp["min_player"].ToString());
        projectData.Add("max_player", resp["max_player"].ToString());
        projectData.Add("description", resp["description"].ToString());
        projNameTitle.GetComponent<TextMeshProUGUI>().text = projectData["name"];

        inputName.GetComponent<TMP_InputField>().text = projectData["name"];
        inputMin.GetComponent<TMP_InputField>().text = projectData["min_player"];
        inputMax.GetComponent<TMP_InputField>().text = projectData["max_player"];
        inputDesc.GetComponent<TMP_InputField>().text = projectData["description"];
    }

    public override void apply()
    {
        int id = int.Parse(args["id"]);
        string projectName = args["project_name"];
        model = GameObject.Find("Model");
        ModelTest modelScr = model.GetComponent<ModelTest>();
        modelScr.find(projectName, applyInServerResponse);
        ConfirmModifyButton butScr = confirmButton.GetComponent<ConfirmModifyButton>();
        butScr.setIdToModify(id);
        RemoveProjectButton rmButScr = removeButton.GetComponent<RemoveProjectButton>();
        rmButScr.setIdToRemove(id);
    }
}
