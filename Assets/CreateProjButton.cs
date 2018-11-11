using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateProjButton : MonoBehaviour {

    [SerializeField]
    GameObject Model;

    [SerializeField]
    GameObject desc;
	// Use this for initialization
	void Start () {
        Model = GameObject.Find("Model");
        gameObject.GetComponent<Button>().onClick.AddListener(click);

    }

    // Update is called once per frame
    void Update () {
		
	}

    void click()
    {
        ///NEED REQUEST DB HERE///
        ModelTest ModelScript = Model.GetComponent<ModelTest>();
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("description" ,desc.GetComponent<TMP_InputField>().text);
        Dictionary<string, string> param = but.getParam();
       
        ModelScript.addCollections(param["name"], param["min"], param["max"], param["description"], "0", "0");
        param.Clear();
        param = new Dictionary<string, string>();
        int id = ModelScript.getNbElement() - 1;
        param.Add("id", id.ToString());


        but.setParams(param);
        but.SendToDispatch();
        ///NEED REQUEST DB HERE///
    }
}
