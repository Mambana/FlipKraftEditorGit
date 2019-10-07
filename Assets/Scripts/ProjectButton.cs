using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProjectButton : MonoBehaviour {

    // Use this for initialization
    private string idOnString;
    private string projectName;
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void setIdOnString(string i)
    {
        idOnString = i;
    }

    public void setProjectName(string name)
    {
        projectName = name;
    }

    void click()
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", idOnString.ToString());
        but.addParam("project_name", projectName);
        but.SendToDispatch();
    }
}
