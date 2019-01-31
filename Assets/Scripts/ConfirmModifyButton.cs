using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmModifyButton : MonoBehaviour {

    // Use this for initialization
    int idToModify;
    GameObject model;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputMin;
    [SerializeField]
    GameObject inputMax;
    [SerializeField]
    GameObject inputDesc;

    void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update () {
      
    }

    public void setIdToModify(int id)
    {
        idToModify = id;
    }

    void click()
    {
        model = GameObject.Find("Model");
        ModelTest modelScr = model.GetComponent<ModelTest>();
        string name = inputName.GetComponent<TMP_InputField>().text;
        string min = inputMin.GetComponent<TMP_InputField>().text;
        string max = inputMax.GetComponent<TMP_InputField>().text;
        string desc = inputDesc.GetComponent<TMP_InputField>().text;
        print(idToModify+ " " + name + " " + min);
        modelScr.updateField(idToModify.ToString(), name, min, max, desc);
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", idToModify.ToString());
        but.SendToDispatch();
    }
}
