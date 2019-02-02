using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmVisualCard : MonoBehaviour
{

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
        model = GameObject.Find("ModelCard");
        ModelCard modelScr = model.GetComponent<ModelCard>();
        string name = inputName.GetComponent<TMP_InputField>().text;
        string desc = inputDesc.GetComponent<TMP_InputField>().text;

        modelScr.updateField(idToModify.ToString(), name, desc, projectId);
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", projectId);
        but.addParam("project_id", projectId);
        but.SendToDispatch();
    }
}
