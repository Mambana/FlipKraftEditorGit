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
    [SerializeField]
    GameObject ressources;
    [SerializeField]
    GameObject rules;
    [SerializeField]
    GameObject operators;
    [SerializeField]
    GameObject opLogic;
    [SerializeField]
    GameObject playerRes;
    [SerializeField]
    GameObject res;
    [SerializeField]
    GameObject playerNb;
    [SerializeField]
    GameObject phases;
    [SerializeField]
    GameObject phasesRules;
    Dictionary<int, string> selParam;
    List<pack> p;
    string selectedRules;
    string relatedPhases;
    string idphases;
    // Start is called before the first frame update
    void Start()
    {
        selParam = new Dictionary<int, string>();
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void mergeToParam(Dictionary<string, string> toMerge)
    {
        int idx;
        
        foreach(KeyValuePair<string, string> par in toMerge)
        {
            if (int.TryParse(par.Key.Substring(4), out idx))
            {
                selParam.Add(idx, par.Value);
                if (par.Key.Contains(":p"))
                    idphases = par.Value;
            }
            else
            {
                idphases = par.Value;
            }
        }
    }

    List<string> getOrderdList()
    {
        List<string> orderdList = new List<string>();
        int idx = 0;

        while (idx != selParam.Count)
        {
            foreach (KeyValuePair<int, string> val in selParam)
            {
                if (idx == val.Key)
                    orderdList.Add(val.Value);
            }
            idx++;
        }
        return (orderdList);
    }
    void appendAllParams()
    {
        Dictionary<string, string> res = ressources.GetComponent<ButtonGetAllRessourcesName>().getSelectedList();
        Dictionary<string, string> pres = playerRes.GetComponent<ButtonGetAllRessourcesName>().getSelectedList();
        Dictionary<string, string> op = operators.GetComponent<ButtonSetArraySelection>().getSelectedList();
        Dictionary<string, string> opl = opLogic.GetComponent<ButtonLogicapOperator>().getSelectedList();
        Dictionary<string, string> pnb = playerNb.GetComponent<ButtonPlayerNumber>().getSelectedList();
        Dictionary<string, string> phas = phases.GetComponent<ButtonSetPhases>().getSelectedList();
        Dictionary<string, string> val = rules.GetComponent<buttonSetRulesList>().getSelectedList();
        mergeToParam(res);
        mergeToParam(pres);
        mergeToParam(op);
        mergeToParam(opl);
        mergeToParam(pnb);
        mergeToParam(phas);
        mergeToParam(val);
        selectedRules = rules.GetComponent<buttonSetRulesList>().getSelectedRules();
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
        appendAllParams();
        p = phases.GetComponent<ButtonSetPhases>().getPhasesPackList(idphases);
        p.Add(rules);
        List<string> param = getOrderdList();
        
        rules.vars = param;
        rules.name = selectedRules;
        string output = JsonConvert.SerializeObject(p);
        print(output);
        modelScr.addRulesToPhases(idphases, projectName,  output, applyInServerResponse);


    }
}
