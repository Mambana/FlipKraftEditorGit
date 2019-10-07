using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRuleController : BasicController
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject resButton;
    [SerializeField]
    GameObject confirmButton;
    [SerializeField]
    GameObject cancelButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void apply()
    {
        int id = int.Parse(args["project_id"]);
        string projectName = args["project_name"];
        ButtonGetAllRessourcesName butScr = resButton.GetComponent<ButtonGetAllRessourcesName>();
        butScr.setProjectName(projectName);
        confirmButton.GetComponent<ConfirmRuleCreationButton>().setProjectId(id.ToString());
        confirmButton.GetComponent<ConfirmRuleCreationButton>().setProjectName(projectName);
        cancelButton.GetComponent<CancelRessourceCreation>().setProjectId(id.ToString());
        cancelButton.GetComponent<CancelRessourceCreation>().setRessourceId(id.ToString());
        cancelButton.GetComponent<CancelRessourceCreation>().setProjectName(projectName);
    }
}
