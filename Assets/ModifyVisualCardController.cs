using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModifyVisualCardController : BasicController
{
    int projectId;

    [SerializeField]
    GameObject elemInList;

    [SerializeField]
    GameObject listOfProj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void apply()
    {
        GameObject model;

        projectId = int.Parse(args["project_id"]);
        model = GameObject.Find("ModelRessource");
        ModelRessource modelScript = model.GetComponent<ModelRessource>();
        Dictionary<int, Dictionary<string, string>> all = modelScript.getAll();
        foreach (KeyValuePair<int, Dictionary<string, string>> project in all)
        {
            if (project.Value["project_id"].Equals(projectId.ToString()))
            {
                GameObject toAdd = Instantiate(elemInList) as GameObject;
                print(project.Value["name"]);
                print(project.Value["description"]);
                toAdd.transform.Find("RessourceName").GetComponent<TextMeshProUGUI>().text = project.Value["name"];
                toAdd.transform.Find("RessourceDesc").GetComponent<TextMeshProUGUI>().text = project.Value["description"];
                toAdd.transform.SetParent(listOfProj.transform, false);
            }

        }
    }
}
