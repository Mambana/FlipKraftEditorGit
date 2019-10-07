using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourceListButton : MonoBehaviour {

    // Use this for initialization
    int projectId;
    string projectName;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCurrentProjectId(int id)
    {
        projectId = id;    
    }

    public void setProjectName(string name)
    {
        projectName = name;
    }

    void click()
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("project_id", projectId.ToString());
        but.addParam("project_name", projectName);
        but.SendToDispatch();
    }
}
