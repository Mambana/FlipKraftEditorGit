using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewProjController : BasicController {

    // Use this for initialization
    [SerializeField]
    private GameObject inputName;
    [SerializeField]
    private GameObject inputMax;
    [SerializeField]
    private GameObject inputDesc;


    void Start () {
    }

    // Update is called once per frame

   
    void Update () {
		
	}

  

    override public void apply()
    {
        if (args != null)
        {
            if (args.ContainsKey("name"))
                inputName.GetComponent<TMP_InputField>().text = args["name"];
            if (args.ContainsKey("max"))
                inputMax.GetComponent<TMP_InputField>().text = args["max"];
            if (args.ContainsKey("description"))
                inputName.GetComponent<TextMeshProUGUI>().text = args["description"];
        }

    }
}
