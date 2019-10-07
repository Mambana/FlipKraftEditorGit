using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelCardRulesCreation : MonoBehaviour
{
    string projectId;
    string id;
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

    public void setProjectId(string id)
    {
        projectId = id;
    }

    public void setCardId(string _id)
    {
        id = _id;
    }

    public void setProjectName(string name)
    {
        projectName = name;
    }
    void click()
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();

        but.addParam("project_id", projectId);
        but.addParam("id", id);
        but.addParam("project_name", projectName);
        but.SendToDispatch();
    }
}
