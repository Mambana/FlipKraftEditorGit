using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class CreatePhaseButton : MonoBehaviour
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

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void applyInServerResponse(string json)
    {
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", projectId);
        but.addParam("project_id", projectId);
        but.addParam("project_name", projectName);
        but.SendToDispatch();
    }

    void click()
    {
        model = GameObject.Find("ModelPhases");
        ModelPhases modelScr = model.GetComponent<ModelPhases>();
        string name = inputName.GetComponent<TMP_InputField>().text;
        string desc = inputDesc.GetComponent<TMP_InputField>().text;
        string priority = inputPriority.GetComponent<TMP_InputField>().text;
        int n;
        bool isNumeric = int.TryParse(priority, out n);
        if (isNumeric == false)
            return ;
        modelScr.addCollections(name, desc, priority, projectId.ToString(), projectName, applyInServerResponse);


    }
}
