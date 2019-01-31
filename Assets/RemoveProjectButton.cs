using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveProjectButton : MonoBehaviour {

    // Use this for initialization
    int idToRemove = 0;
    GameObject model;
    
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setIdToRemove(int id)
    {
        idToRemove = id;
    }

    void click()
    {
        model = GameObject.Find("Model");
        ModelTest modelScr = model.GetComponent<ModelTest>();

        modelScr.removeElem(idToRemove);

        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.SendToDispatch();
    }

}
