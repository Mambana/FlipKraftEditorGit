using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ModifyCardController : BasicController {

    // Use this for initialization
    // Use this for initialization
    GameObject model;
    [SerializeField]
    GameObject confirmButton;
    [SerializeField]
    GameObject removeButton;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputDesc;
    [SerializeField]
    GameObject projRessource;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void apply()
    {
       // int id = int.Parse(args["id"]);
        model = GameObject.Find("ModelCard");
        ModelCard modelScr = model.GetComponent<ModelCard>();
      //  Dictionary<string, string> cardData = modelScr.find(id);
        //projNameTitle.GetComponent<TextMeshProUGUI>().text = modelScr.find(id, "name");

        inputName.GetComponent<TMP_InputField>().text = "";

        inputDesc.GetComponent<TMP_InputField>().text = "";

        ConfirmModifyCard butScr = confirmButton.GetComponent<ConfirmModifyCard>();
       // butScr.setIdToModify(id);
        butScr.setProjectId(args["project_id"]);
        CancelRessourceCreation cancel = removeButton.GetComponent<CancelRessourceCreation>();
        cancel.setProjectId(args["project_id"]);
       

    }
}
