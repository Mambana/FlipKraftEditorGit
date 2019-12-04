using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;

public class pack
{
    public string name;
    public List<string> vars;
}
public class ConfirmRuleCreationButton : MonoBehaviour
{
    string projectId;
    string projectName;
    GameObject model;
    List<string> rulVar;
    List<pack> p;
    [SerializeField]
    GameObject relatedPhases;
    [SerializeField]
    string selectedRules;
    [SerializeField]
    List<GameObject> dropDowns;
    string idphases;
    Dictionary<string, string> keyMap;
    // Start is called before the first frame update
    void Start()
    {
        rulVar = new List<string>();
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setParamKeys(Dictionary<string,string> keys)
    {
        keyMap = keys;
    }
    void setListDropDown(List<GameObject> var)
    {
        dropDowns = var;
    }
    public void setProjectId(string id)
    {
        projectId = id;
    }

    public void setProjectName(string name)
    {
        projectName = name;
    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void applyInServerResponse(string json)
    {
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", projectId);
        but.addParam("project_id", projectId);
        but.addParam("project_name", projectName);
        but.SendToDispatch();
    }



 
    List<string> appendAllParams()
    { 
        foreach(GameObject drop in dropDowns)
        {
            int idx = drop.GetComponent<Dropdown>().value;
            List<Dropdown.OptionData> menuOptions = drop.GetComponent<Dropdown>().options;
            print(menuOptions[idx].text);
            rulVar.Add(drop.GetComponent<SelectionFromAPI>().getObjectId(menuOptions[idx].text));
        }
        return (rulVar);
    }

    string getRelatedPhases()
    {
        int idx = relatedPhases.GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuOptions = relatedPhases.GetComponent<Dropdown>().options;
        return (relatedPhases.GetComponent<SelectionFromAPI>().getPhasesId(menuOptions[idx].text));
    }
    void click()
    {
        model = GameObject.Find("ModelPhases");
        pack rules = new pack();


      
        ModelPhases modelScr = model.GetComponent<ModelPhases>();
       // string name = inputName.GetComponent<TMP_InputField>().text;
        //string desc = inputDesc.GetComponent<TMP_InputField>().text;
        int n;
        string[] descs = new string[1];
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        idphases = getRelatedPhases();
        p = relatedPhases.GetComponent<SelectionFromAPI>().getPackList(idphases);
       
        List<string> param =appendAllParams();
        
        rules.vars = param;
        rules.name = selectedRules;
        p.Add(rules);
        string output = JsonConvert.SerializeObject(p);
        print(output);
        modelScr.addRulesToPhases(idphases, projectName,  output, applyInServerResponse);


    }
}
