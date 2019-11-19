using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmModifyButton : MonoBehaviour {

    // Use this for initialization
    int idToModify;
    string projectName;
    GameObject model;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputMin;
    [SerializeField]
    GameObject inputMax;
    [SerializeField]
    GameObject inputDesc;
    [SerializeField]
    GameObject wrong;
    [SerializeField]
    GameObject paper;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject inputValuePopPup;
    void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update () {
      
    }

    public void setIdToModify(int id)
    {
        idToModify = id;
    }

    public void setProjectName(string name)
    {
        projectName = name;
    }

    public void CallDispatcher(string json)
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", idToModify.ToString());
        but.addParam("project_name", projectName);
        but.SendToDispatch();
    }

    void click()
    {
        int nb;
        GameObject error;
        GameObject disable;
        model = GameObject.Find("Model");
        ModelTest modelScr = model.GetComponent<ModelTest>();
        string name = inputName.GetComponent<TMP_InputField>().text;
       // string min = inputMin.GetComponent<TMP_InputField>().text;
        string max = inputMax.GetComponent<TMP_InputField>().text;
        string desc = inputDesc.GetComponent<TMP_InputField>().text;
        string turn_game = "false";
        string async_game = "false";
        if (int.TryParse(max, out nb) == false)
        {
            error = Instantiate(wrong) as GameObject;
            disable = Instantiate(paper) as GameObject;
            error.GetComponentInChildren<ConfirmErrorBtn>().setParent(error);
            error.GetComponentInChildren<ConfirmErrorBtn>().setDisableScreen(disable);
            error.transform.SetParent(canvas.transform, false);
            disable.transform.SetParent(canvas.transform, false);
            disable.transform.SetSiblingIndex(98);
            error.transform.SetSiblingIndex(99);
        }
        else
         modelScr.updateField(projectName, name,  max, desc, CallDispatcher, async_game, turn_game);
       
    }
}
