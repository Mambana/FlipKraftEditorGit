using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRuleController : BasicController
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject dropdown;

    [SerializeField]
    GameObject confirmButton;
    [SerializeField]
    GameObject cancelButton;
    [SerializeField]
    List<GameObject> dropDowns;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void apply()
    {
        print("applyed");
        int id = int.Parse(args["project_id"]);
        string projectName = args["project_name"];
        dropdown.GetComponent<SelectionFromAPI>().setProjectName(projectName);
        dropdown.GetComponent<SelectionFromAPI>().apply();
        foreach (GameObject drop in dropDowns)
        {
            drop.GetComponent<SelectionFromAPI>().setProjectName(projectName);
            drop.GetComponent<SelectionFromAPI>().apply();
        }
        confirmButton.GetComponent<ConfirmRuleCreationButton>().setProjectId(id.ToString());
        confirmButton.GetComponent<ConfirmRuleCreationButton>().setProjectName(projectName);
        cancelButton.GetComponent<CancelRessourceCreation>().setProjectId(id.ToString());
        cancelButton.GetComponent<CancelRessourceCreation>().setRessourceId(id.ToString());
        cancelButton.GetComponent<CancelRessourceCreation>().setProjectName(projectName);
    
            }
}
