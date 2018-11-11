using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmModifyRessource : MonoBehaviour {

    // Use this for initialization
    int idToModify;
    string projectId;
    GameObject model;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputDesc;

  
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setIdToModify(int id)
    {
        idToModify = id;
    }

    public void setProjectId(string id)
    {
        projectId = id;
    }

    void click()
    {
        model = GameObject.Find("ModelRessource");
        ModelRessource modelScr = model.GetComponent<ModelRessource>();

        modelScr.updateField(idToModify, "name", inputName.GetComponent<TMP_InputField>().text);
        modelScr.updateField(idToModify, "description", inputDesc.GetComponent<TMP_InputField>().text);

        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", idToModify.ToString());
        but.addParam("project_id", projectId);
        but.SendToDispatch();
    }
}
