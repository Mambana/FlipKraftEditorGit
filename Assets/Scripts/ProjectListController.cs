using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class ProjectListController : BasicController {
 
    GameObject model;
    [SerializeField]
    GameObject listUi;
    [SerializeField]
    GameObject elemInList;
    [SerializeField]
    GameObject listOfProj;
    [SerializeField]
    List<GameObject> buttonList;
	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void applyInServerResponse(string json)
    {
        Dictionary<int, Dictionary<string, string>> allProj = new Dictionary<int, Dictionary<string, string>>();
        List<object> respList = DeserializeJson<List<object>>(json);
        int i = 0;
        foreach (GameObject button in buttonList)
        {           
                Destroy(button);
        }
        buttonList.Clear();
        allProj.Clear();
        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> projectData = new Dictionary<string, string>();

            projectData.Add("name", resp["name"].ToString());
            projectData.Add("async_game", resp["async_game"].ToString());
            projectData.Add("turn_game", resp["turn_game"].ToString());
            projectData.Add("min_player", resp["min_player"].ToString());
            projectData.Add("max_player", resp["max_player"].ToString());
            projectData.Add("description", resp["description"].ToString());
            projectData.Add("id", resp["id"].ToString());
            allProj.Add(i, projectData);
            i++;
        }
       
        foreach (KeyValuePair<int, Dictionary<string, string>> project in allProj)
        {
            GameObject toAdd = Instantiate(elemInList) as GameObject;
            toAdd.transform.Find("ProjName").GetComponent<TextMeshProUGUI>().text = project.Value["name"];
            toAdd.transform.Find("ProjDesc").GetComponent<TextMeshProUGUI>().text = project.Value["description"];
            toAdd.GetComponent<ProjectButton>().setIdOnString(project.Value["id"]);
            toAdd.GetComponent<ProjectButton>().setProjectName(project.Value["name"]);
            buttonList.Add(toAdd);
            if (listOfProj)
                toAdd.transform.SetParent(listOfProj.transform, false);
           
        }
    }

    override public void apply()
    {
        model = GameObject.Find("Model");
        ModelTest modelScript = model.GetComponent<ModelTest>();
        modelScript.getAll(applyInServerResponse);
    }
}
