using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemovePhaseButton : MonoBehaviour
{
    int idToRemove = 0;
    string projectId = "";
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

    public void setIdToRemove(int id)
    {
        idToRemove = id;
    }

    public void setProjectId(string id)
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
        if (but)
        {
            but.addParam("project_id", projectId);
            but.addParam("id", projectId);
            but.addParam("project_name", projectName);
            but.SendToDispatch();
        }
        else
            Destroy(gameObject.transform.parent.gameObject);
    }

    void click()
    {
        model = GameObject.Find("ModelPhases");
        ModelPhases modelScr = model.GetComponent<ModelPhases>();
        modelScr.removeElem(idToRemove, projectName, callDispatcher); 
    }
}
