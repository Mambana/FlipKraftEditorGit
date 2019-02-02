using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyCardButton : MonoBehaviour {

    string idToModify;
    string projectId;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setIdToModify(string id)
    {
        idToModify = id;
        print("ok");
    }

    public void setProjectId(string id)
    {
        projectId = id;
    }



    void click()
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", idToModify);
        but.addParam("project_id", projectId);
        but.SendToDispatch();
    }
}
