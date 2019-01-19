using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelRessourceCreation : MonoBehaviour
{
    
    string projectId;
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

    void click()
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();

        but.addParam("project_id", projectId);
        but.SendToDispatch();
    }
}
