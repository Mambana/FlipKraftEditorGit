using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModifyRessourceButton : MonoBehaviour {

    // Use this for initialization
    string idToModify;
    string projectId;
    string projectName;
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setIdToModify(string id)
    {
        idToModify = id;
    }

    public void setProjectId(string id)
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
        but.addParam("id", idToModify);
        but.addParam("project_id", projectId);
        but.addParam("project_name", projectName);
        but.SendToDispatch();
    }
}
