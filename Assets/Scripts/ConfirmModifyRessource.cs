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
    ImageHandler imageHandler;
    int imgId;
  
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

    public void setImageId(int id)
    {
        imgId = id;
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
        model = GameObject.Find("ModelRessource");
        ModelRessource modelScr = model.GetComponent<ModelRessource>();
        string name = inputName.GetComponent<TMP_InputField>().text;
        string desc = inputDesc.GetComponent<TMP_InputField>().text;
        modelScr.updateField(idToModify.ToString(), projectId, name, desc, imgId.ToString(), CallDispatcher);
        
    }
}
