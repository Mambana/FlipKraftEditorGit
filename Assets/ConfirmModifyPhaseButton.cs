using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmModifyPhaseButton : MonoBehaviour
{
    int idToModify;
    string projectId;
    GameObject model;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputDesc;
    [SerializeField]
    GameObject inputPriority;

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

    public void CallDispatcher(string json)
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", projectId);
        but.addParam("project_id", projectId);
        but.SendToDispatch();
    }

    void click()
    {
        print ("phase id = " + idToModify.ToString());
        model = GameObject.Find("ModelPhases");
        ModelPhases modelScr = model.GetComponent<ModelPhases>();
        string name = inputName.GetComponent<TMP_InputField>().text;
        string desc = inputDesc.GetComponent<TMP_InputField>().text;
        string priority = inputPriority.GetComponent<TMP_InputField>().text;
        int n;
        print(name);
        print(priority);
        bool isNumeric = int.TryParse(priority, out n);
        if (isNumeric == false)
            return;
        modelScr.updateField(idToModify.ToString(), projectId, name, desc, priority, CallDispatcher);

    }
}
