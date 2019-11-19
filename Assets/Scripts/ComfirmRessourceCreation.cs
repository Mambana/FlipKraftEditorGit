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
    string projectName;
    int imgId;
    GameObject model;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputDesc;
    [SerializeField]
    private GameObject ressourceImage;
    [SerializeField]
    private GameObject inputPlayerValue;
    private ImageHandler imageHandler;


    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        imageHandler = GameObject.Find("ImageHandler").GetComponent<ImageHandler>();
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

    public void setImageId(int id)
    {
        imgId = id;
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
        model = GameObject.Find("ModelRessource");
        ModelRessource modelScr = model.GetComponent<ModelRessource>();
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", projectId);
        but.addParam("project_id", projectId);
        but.addParam("project_name", projectName);
        print(modelScr.getNbElement());
       
        but.SendToDispatch();
    }

    void click()
    {
        model = GameObject.Find("ModelRessource");
        ModelRessource modelScr = model.GetComponent<ModelRessource>();
        string name = inputName.GetComponent<TMP_InputField>().text;
        string desc = inputDesc.GetComponent<TMP_InputField>().text;
        string pvalue = inputPlayerValue.GetComponent<TMP_InputField>().text;
        modelScr.addCollections(name, desc, projectId, projectName, applyInServerResponse, imgId.ToString(), pvalue);
      
    }
}
