using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
public class ConfirmModifyPhaseButton : MonoBehaviour
{
    int idToModify;
    string projectId;
    string projectName;
    GameObject model;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputDesc;
    [SerializeField]
    GameObject inputPriority;
    [SerializeField]
    GameObject parent;
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

    public void setProjectName(string name)
    {
        projectName = name;
    }
    public void CallDispatcher(string json)
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", projectId);
        but.addParam("project_id", projectId);
        but.addParam("project_name", projectName);
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
        List<pack> packList = parent.GetComponent<ModifyPhaseController>().getPackList();
        string json;
        if (packList.Count == 0)
             json = JsonConvert.SerializeObject(new List<pack>());
         json = JsonConvert.SerializeObject(packList);
        print(json);
      
        modelScr.updateField(idToModify.ToString(), projectId, projectName,  name, desc, priority, json, CallDispatcher);

    }
}
