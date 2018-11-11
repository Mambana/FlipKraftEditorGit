using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdtAddProjDescController : BasicController
{
    [SerializeField]
    private GameObject prevButton;

    [SerializeField]
    private GameObject nextButton;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    override public void apply()
    {
        ButtonListener prevButListener = prevButton.GetComponent<ButtonListener>();
        ButtonListener nextButListener = nextButton.GetComponent<ButtonListener>();
            prevButListener.setParams(args);
            nextButListener.setParams(args);
        
        
        //scr.setParams(args);
    
    }
}
