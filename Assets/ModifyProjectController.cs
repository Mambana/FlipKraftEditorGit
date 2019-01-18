using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public override void apply()
    {
        int id = int.Parse(args["id"]);
        model = GameObject.Find("Model");
        ModelTest modelScr = model.GetComponent<ModelTest>();
        Dictionary<string, string> projectData = modelScr.find(id);

        projNameTitle.GetComponent<TextMeshProUGUI>().text = projectData["name"];

        inputName.GetComponent<TMP_InputField>().text = projectData["name"];
        inputMin.GetComponent<TMP_InputField>().text = projectData["min_player"];
        inputMax.GetComponent<TMP_InputField>().text = projectData["max_player"];
        inputDesc.GetComponent<TMP_InputField>().text = projectData["description"];

        ConfirmModifyButton butScr = confirmButton.GetComponent<ConfirmModifyButton>();
        butScr.setIdToModify(id);

        RemoveProjectButton rmButScr = removeButton.GetComponent<RemoveProjectButton>();
        rmButScr.setIdToRemove(id);

    }
}
