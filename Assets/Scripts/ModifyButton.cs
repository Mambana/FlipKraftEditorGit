using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyButton : MonoBehaviour {

    // Use this for initialization
    int idToModify;
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
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", idToModify.ToString());
        but.SendToDispatch();
    }
}
