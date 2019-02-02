using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveCardButton : MonoBehaviour {


    int idToRemove = 0;
    string projectId = "";
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
        model = GameObject.Find("ModelCard");
        ModelCard modelScr = model.GetComponent<ModelCard>();

        modelScr.removeElem(idToRemove);
        print(projectId);
        
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("project_id", projectId);
        but.addParam("id", projectId);
        but.SendToDispatch();
    }
}
