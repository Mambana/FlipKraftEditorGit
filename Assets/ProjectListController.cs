using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectListController : BasicController {
 
    GameObject model;
    [SerializeField]
    GameObject listUi;
    [SerializeField]
    GameObject elemInList;
    [SerializeField]
    GameObject listOfProj;
	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override public void apply()
    {
        model = GameObject.Find("Model");
        print("model" + model.GetComponent<ModelTest>().getAll());
        ModelTest modelScript = model.GetComponent<ModelTest>();
        Dictionary<int, Dictionary<string, string>> all = modelScript.getAll();
     
        foreach (KeyValuePair<int, Dictionary<string, string>> project in all)
        {
            GameObject toAdd = Instantiate(elemInList) as GameObject;
            print(project.Value["name"]);
            print(project.Value["description"]);
            toAdd.transform.Find("ProjName").GetComponent<TextMeshProUGUI>().text = project.Value["name"];
            toAdd.transform.Find("ProjDesc").GetComponent<TextMeshProUGUI>().text = project.Value["description"];
            toAdd.GetComponent<ProjectButton>().setIdOnString(project.Value["id"]);
            toAdd.transform.SetParent(listOfProj.transform, false);


        }
        // add button here
    }
}
