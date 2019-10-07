using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfirmVisualCard : MonoBehaviour
{

    // Use this for initialization
    int idToModify;
    string projectId;
    string projectName;
    GameObject model;
    GameObject modelAssoc;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputDesc;
    [SerializeField]
    private List<Dictionary<string, string>> assocListToModify;

    public void addAssocToModify(string pId, string cId, string rId, string pX, string pY, string val, string aId = null)
    {
        Dictionary<string, string> assoc = new Dictionary<string, string>();
        assoc["project_id"] = pId;
        assoc["card_id"] = cId;
        assoc["ressource_id"] = rId;
        assoc["posX"] = pX;
        assoc["posY"] = pY;
        assoc["assoc_id"] = aId;
        assoc["value"] = val;
        assocListToModify.Add(assoc);
    }

    void Start()
    {
        assocListToModify = new List<Dictionary<string, string>>();
        gameObject.GetComponent<Button>().onClick.AddListener(click);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setIdToModify(int id)
    {
        idToModify = id;
    }

    public void setProjectName(string name)
    {
        projectName = name;
    }
    public void setProjectId(string id)
    {
        projectId = id;
    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    void CallDispatcher(string json)
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();

        but.addParam("id", projectId);
        but.addParam("project_id", projectId);
        but.addParam("project_name", projectName);
        but.SendToDispatch();
    }

    void sendList(string json)
    {
        
        modelAssoc = GameObject.Find("ModelAssociation");
        ModelAssociation modelScr = modelAssoc.GetComponent<ModelAssociation>();
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        string cid = resp["id"].ToString();

        foreach (Dictionary<string,string> l in assocListToModify)
        {
            print(l);
            if (l["value"].Equals(""))
                l["value"] = "0";
            if (l["assoc_id"] == null)
          modelScr.addCollections(l["value"], l["project_id"], projectName, cid, l["ressource_id"], 
              l["posX"],
              l["posY"]);
        else
          modelScr.updateField(l["assoc_id"], l["value"], l["project_id"], projectName, cid, l["ressource_id"],
              l["posX"],
              l["posY"]);
        }
        CallDispatcher(json);
    }

    void click()
    {
        model = GameObject.Find("ModelCard");
        ModelCard modelScr = model.GetComponent<ModelCard>();
        string name = inputName.GetComponent<TMP_InputField>().text;
        string desc = inputDesc.GetComponent<TMP_InputField>().text;
        if (name.Equals(""))
            name = Guid.NewGuid().ToString();
        if (desc.Equals(""))
            desc = Guid.NewGuid().ToString();
        if (idToModify == -1)
            modelScr.addCollections(name, desc, projectId, projectName, sendList);
        else
          modelScr.updateField(idToModify.ToString(), name, desc, projectId, projectName, sendList);
        
      
     
    }
}
