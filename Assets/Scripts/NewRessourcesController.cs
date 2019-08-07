using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewRessourcesController : BasicController
{
    GameObject model;
    [SerializeField]
    GameObject confirmButton;
    [SerializeField]
    GameObject cancelButton;

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
       

        ComfirmRessourceCreation butScr = confirmButton.GetComponent<ComfirmRessourceCreation>();
        butScr.setProjectId(args["project_id"]);
        CancelRessourceCreation cancelBut = cancelButton.GetComponent<CancelRessourceCreation>();
        cancelBut.setProjectId(args["project_id"]);
        cancelBut.setRessourceId(args["project_id"]);
    }
}
