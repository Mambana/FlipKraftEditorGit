using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyRulesButton : MonoBehaviour
{
    int projectId;
    int ruleId;
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

    public void click()
    {
            ButtonListener but = gameObject.GetComponent<ButtonListener>();
            but.addParam("id", ruleId.ToString());
            but.addParam("project_id", projectId.ToString());
            but.SendToDispatch();
        
    }
}
