using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveRessourceButton : MonoBehaviour {

    int idToRemove;
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

    public void setIdToRemove(int id)
    {
        idToRemove = id;
    }

    public void setProjectId(string id)
    {
        projectId = id;
    }

    void click()
    {
        model = GameObject.Find("ModelRessource");
        ModelRessource modelScr = model.GetComponent<ModelRessource>();

        modelScr.removeElem(idToRemove);

        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("project_id", projectId);
        but.SendToDispatch();
    }
}
