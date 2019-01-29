using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextNewProjButton : MonoBehaviour {
     
    [SerializeField]
    private GameObject inputName;
    [SerializeField]
    private GameObject inputMin;
    [SerializeField]
    private GameObject inputMax;
    [SerializeField]
    private GameObject inputDesc;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(click);

    }

    // Update is called once per frame
    void Update () {
      
    }

    void click()
    {
       /* inputName = GameObject.Find("InputProjectName");
        inputMin = GameObject.Find("InputMinPlayer");
        inputMax = GameObject.Find("InputMaxPlayer");*/
       
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        
        if (but != null)
        {
            but.addParam("name", inputName.GetComponent<TMP_InputField>().text);   
            but.addParam("min", inputMin.GetComponent<TMP_InputField>().text);
            but.addParam("max", inputMax.GetComponent<TMP_InputField>().text);
          //  but.addParam("description", inputDesc.GetComponent<TextMeshProUGUI>().text);
        }
        but.SendToDispatch();
    }
}
