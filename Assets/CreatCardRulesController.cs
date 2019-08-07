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
        ButtonGetAllRessourcesName butScr = resButton.GetComponent<ButtonGetAllRessourcesName>();
        butScr.setProjectId(id);
        confirmButton.GetComponent<ConfirmCardRuleCreationButton>().setProjectId(id.ToString());
        confirmButton.GetComponent<ConfirmCardRuleCreationButton>().setCardId(cardId.ToString());
        cancelButton.GetComponent<CancelCardRulesCreation>().setProjectId(id.ToString());
        cancelButton.GetComponent<CancelCardRulesCreation>().setCardId(cardId.ToString());
        resButton.GetComponent<ButtonGetAllRessourcesName>().setProjectId(id);
    }
}
