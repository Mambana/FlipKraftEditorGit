using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class SelectionFromAPI : MonoBehaviour
{
    [SerializeField]
    bool rp = false;
    [SerializeField]
    string projectName;
    [SerializeField]
    string modelName;
    GameObject model;
    [SerializeField]
    GameObject PhasesDropDown;
    Dictionary<string, string> phases;
    Dictionary<string, List<pack>> allPack;
    [SerializeField]
    List<string> hardParam;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    public void apply()
        {
        allPack = new Dictionary<string, List<pack>>();
        phases = new Dictionary<string, string>();
        model = GameObject.Find(modelName);
        if (modelName.Equals("ModelPhases"))
            model.GetComponent<ModelPhases>().getAll(projectName, applyForPhases);
        else if (modelName.Equals("ModelRessource"))
            model.GetComponent<ModelRessource>().getAll(projectName, applyForPhases);
        else if (modelName.Equals("Value"))
        {
            int i = 0;
            hardParam = new List<string>();
            while (i != 30)
            {
                hardParam.Add(i.ToString());
                i++;
            }
            addHardParamToDropDown();
        }
        else
            addHardParamToDropDown();

    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void addHardParamToDropDown()
    {
        Dropdown dropdownEventIDs = PhasesDropDown.GetComponent<Dropdown>();
        foreach (string param in hardParam)
        {
            dropdownEventIDs.AddOptions(new List<string> { param});
        }
    }
    public void applyForPhases(string json)
    {
        print(json);
        Dictionary<int, Dictionary<string, string>> allRessource = new Dictionary<int, Dictionary<string, string>>();
        List<object> respList = DeserializeJson<List<object>>(json);
        Dropdown dropdownEventIDs = PhasesDropDown.GetComponent<Dropdown>();
        int i = 0;

        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> ressourceData = new Dictionary<string, string>();

            if (rp)
            {
               
                if (resp.ContainsKey("player_value"))
                {
                    if (resp["player_value"] != null)
                    {
                        print(resp["player_value"].ToString());
                        ressourceData.Add("name", resp["name"].ToString());
                        ressourceData.Add("id", resp["id"].ToString());
                        
                    }
                }
            }
            else
            {
                ressourceData.Add("name", resp["name"].ToString());
                ressourceData.Add("id", resp["id"].ToString());
                if (resp.ContainsKey("pack"))
                {
                    if (!resp["pack"].ToString().Equals("{}"))
                    {
                        ressourceData.Add("pack", resp["pack"].ToString());
                        if (!allPack.ContainsKey(resp["id"].ToString()))
                         allPack.Add(resp["id"].ToString(), DeserializeJson<List<pack>>(resp["pack"].ToString()));
                    }
                }
            }
            allRessource.Add(i, ressourceData);
            i++;
        }

        foreach (KeyValuePair<int, Dictionary<string, string>> res in allRessource)
        {
            if (res.Value.ContainsKey("name") && res.Value.ContainsKey("id"))
            {
                if (!phases.ContainsKey(res.Value["name"])) 
                    phases.Add(res.Value["name"], res.Value["id"]);
            }
        }
        foreach (KeyValuePair<string, string> p in phases)
        {
            dropdownEventIDs.AddOptions(new List<string> { p.Key });
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public string getObjectId(string key)
    {
        if (gameObject.name.Equals("keys"))
        {
            if (phases.ContainsKey(key))
                return (phases[key]);
        }
            return (key);
    }

    public List<pack> getPackList(string pId)
    {
        if (allPack.ContainsKey(pId))
            return (allPack[pId]);
        else return (new List<pack>());
    }
    public string getPhasesId(string name)
    {
        return (phases[name]);
    }
    public void setProjectName(string pro)
    {
        projectName = pro;
    }
}
