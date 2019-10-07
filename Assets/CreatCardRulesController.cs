using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatCardRulesController : BasicController
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
        int cardId = int.Parse(args["card_id"]);
        string projectName = args["project_name"];
        ButtonGetAllRessourcesName butScr = resButton.GetComponent<ButtonGetAllRessourcesName>();
        butScr.setProjectName(projectName);
        confirmButton.GetComponent<ConfirmCardRuleCreationButton>().setProjectId(id.ToString());
        confirmButton.GetComponent<ConfirmCardRuleCreationButton>().setCardId(cardId.ToString());
        confirmButton.GetComponent<ConfirmCardRuleCreationButton>().setProjectName(projectName);
        cancelButton.GetComponent<CancelCardRulesCreation>().setProjectId(id.ToString());
        cancelButton.GetComponent<CancelCardRulesCreation>().setCardId(cardId.ToString());
        cancelButton.GetComponent<CancelCardRulesCreation>().setProjectName(projectName);
    }
}
