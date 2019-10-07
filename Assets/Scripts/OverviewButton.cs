using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverviewButton : MonoBehaviour {

    // Use this for initialization
    int projectId;
    string projectName;
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    public void setCurrentProjectId(int id)
    {
        projectId = id;
    }
    // Update is called once per frame
    public void setProjectName(string name)
    {
        projectName = name;
    }

    void click()
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        // Dictionary<string, string> param = new Dictionary<string, string>();


        but.addParam("id", projectId.ToString());
        print(but.getParam());
        but.SendToDispatch();
    }

    void Update()
    {

    }
}
