using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCardButton : MonoBehaviour {

    // Use this for initialization

    int idToModify;
    int projectId;
    GameObject Model;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        Model = GameObject.Find("ModelCard");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setIdToModify(int id)
    {
        idToModify = id;
    }

    public void setProjectId(int id)
    {
        projectId = id;
    }

    void click()
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        ModelCard ModelScr = Model.GetComponent<ModelCard>();
      //  ModelScr.addCollections("ENTER new card name", "ENTER new description", projectId.ToString());
      //  but.addParam("id", (ModelScr.getNbElement()).ToString());
        but.addParam("project_id", projectId.ToString());
        but.addParam("card_id", idToModify.ToString());
        but.SendToDispatch();
    }
}
