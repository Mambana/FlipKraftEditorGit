using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyCardRulesButton : MonoBehaviour
{
    int projectId;
    int ruleId;
    int cardId;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setProjectId(int id)
    {
        projectId = id;
    }

    public void setRuleId(int id)
    {
        ruleId = id;
    }

    public void setCardId(int id)
    {
        cardId = id;
    }

    public void click()
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("rule_id", ruleId.ToString());
        but.addParam("project_id", projectId.ToString());
        but.addParam("card_id", cardId.ToString());
        but.SendToDispatch();

    }
}
