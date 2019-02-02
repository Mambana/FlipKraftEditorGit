using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EdtHomeControl : BasicController{

    // Use this for initialization
    [SerializeField]
    protected GameObject testTxt;

    //List<string> args;

	void Start () {
		
	}

   

    // Update is called once per frame
    override public void apply()
    {
        //testTxt.GetComponent<TextMeshProUGUI>().text = args[0];
    }
    void Update () {
		
	}
}
