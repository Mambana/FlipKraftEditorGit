using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateRessourceButton : MonoBehaviour {

    // Use this for initialization
   
    int idToModify;
    int projectId;
    string projectName;

    GameObject Model;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        Model = GameObject.Find("ModelRessource");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setIdToModify(int id)
    {
        idToModify = id;
    }

    public void setProjectName(string name)
    {
        projectName = name;
    }
    public void setProjectId(int id)
    {
        projectId = id;
    }

    void click()
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        ModelRessource ModelScr = Model.GetComponent<ModelRessource>();
        but.addParam("project_id", projectId.ToString());
        but.addParam("project_name", projectName);
        but.SendToDispatch();
    }
}
