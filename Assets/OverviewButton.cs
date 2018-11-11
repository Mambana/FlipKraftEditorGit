using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverviewButton : MonoBehaviour {

    // Use this for initialization
    int projectId;

	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    public void setCurrentProjectId(int id)
    {
        projectId = id;
    }
    // Update is called once per frame
    

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
