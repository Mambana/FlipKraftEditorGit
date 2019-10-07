using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveRuleButton : MonoBehaviour
{
    // Start is called before the first frame update
    int id;
    int projectId;
    string projectName;
    GameObject model;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setIdToRemove(int _id)
    {
        id = _id;
    }

    public void setProjectId(int id)
    {
        projectId = id;
    }

    public void setProjectName(string name)
    {
        projectName = name;
    }

    void callDispatcher(string json)
    {
        print("call");
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("project_id", projectId.ToString());
        but.addParam("id", projectId.ToString());
        but.SendToDispatch();
    }

    void click()
    {
        model = GameObject.Find("ModelRules");
        ModelRules modelScr = model.GetComponent<ModelRules>();
        modelScr.removeElem(id, projectName, callDispatcher);
    }

}
