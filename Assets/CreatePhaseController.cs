using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePhaseController : BasicController
{
    [SerializeField]
    GameObject confirmButton;
    [SerializeField]
    GameObject cancelButton;
    int projectId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void apply()
    {
        CreatePhaseButton butScr = confirmButton.GetComponent<CreatePhaseButton>();
        butScr.setProjectId(args["project_id"]);
        butScr.setProjectName(args["project_name"]);
        CancelRessourceCreation cancelBut = cancelButton.GetComponent<CancelRessourceCreation>();
        cancelBut.setProjectId(args["project_id"]);
        cancelBut.setRessourceId(args["project_id"]);
    }
}
