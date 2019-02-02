using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComfirmRessourceCreation : MonoBehaviour
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
        but.SendToDispatch();
    }

    void click()
    {
        model = GameObject.Find("ModelRessource");
        ModelRessource modelScr = model.GetComponent<ModelRessource>();
        string name = inputName.GetComponent<TMP_InputField>().text;
        string desc = inputDesc.GetComponent<TMP_InputField>().text;

        modelScr.addCollections(name, desc, projectId.ToString(), applyInServerResponse);
      
    }
}
