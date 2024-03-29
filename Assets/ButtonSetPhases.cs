﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class ButtonSetPhases : MonoBehaviour
{
    // Start is called before the first frame update
    private int projectId;
    private string projectName;
    [SerializeField]
    GameObject objList;
    private List<string> stringList;
    [SerializeField]
    GameObject m_toggle;
    private List<string> toSend;
    [SerializeField]
    bool playerRessources;
    [SerializeField]
    GameObject rulesTxt;

    private Dictionary<string, string> id_list;
    private Dictionary<string, string> phases;
    private List<GameObject> togList;
    private Dictionary<string, string> selectedOp;
    private List<string> opKeyList;
    private int maxOpKey = 0;
    private Dictionary<string,List<pack>> phasesRules; 
    Toggle LastToggle;
    string originalRules;

    string parser;
    int ofset;
    int activeToggle = 0;
    void Start()
    {
        phasesRules = new Dictionary<string, List<pack>>();
        id_list = new Dictionary<string, string>();
        phases = new Dictionary<string, string>();
        togList = new List<GameObject>();
        toSend = new List<string>();
        stringList = new List<string>();
        selectedOp = new Dictionary<string, string>();
        LastToggle = null;
        opKeyList = new List<string>();

            parser = ":p";
            ofset = 5;
       
    }

    // Update is called once per frame
    void Update()
    {

    }


    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    private void parseRulesOpText()
    {
        string rulesString = rulesTxt.GetComponent<TextMeshProUGUI>().text;
        string key;
        int lastIdx = 0;

        while ((lastIdx = rulesString.IndexOf(parser, lastIdx)) != -1)
        {
            if (lastIdx >= 0)
            {
                key = rulesString.Substring(lastIdx, ofset);
                lastIdx += ofset;
                if (!opKeyList.Contains(key))
                    opKeyList.Add(key);
            }

        }
    }
    public void addRulesOpItem(string toAdd)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        foreach (string item in opKeyList)
        {
            if (!selectedOp.ContainsKey(item))
            {
                selectedOp.Add(item, toAdd);
                return;
            }
        }
        foreach (GameObject tog in togList)
        {
            if (tog.GetComponentInChildren<Text>().text.Equals(selectedOp.First().Value) &&
                tog.GetComponent<Toggle>().isOn == true)
            {
                tog.GetComponent<Toggle>().isOn = false;
                break;
            }

        }
        string lostKey = selectedOp.First().Key;
        selectedOp.Remove(lostKey);
        foreach (KeyValuePair<string, string> pair in selectedOp)
        {
            dic.Add(pair.Key, pair.Value);
        }
        dic.Add(lostKey, toAdd);
        selectedOp.Clear();
        selectedOp = dic;
        updateRulesTextForOp();

    }
    public string updateRulesTextForOp()
    {
        string rulesString = originalRules;
        foreach (KeyValuePair<string, string> op in selectedOp)
        {

            rulesString = rulesString.Replace(op.Key, op.Value);
        }
        rulesTxt.GetComponent<TextMeshProUGUI>().text = rulesString;
        return (rulesString);
    }


    public void applyinResponse(string json)
    {
        Dictionary<int, Dictionary<string, string>> allRessource = new Dictionary<int, Dictionary<string, string>>();
        List<object> respList = DeserializeJson<List<object>>(json);
        List<pack> packList;

        
        //   destroyList(ressources);
        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            if (resp.ContainsKey("name"))
                stringList.Add(resp["name"].ToString());
            if (resp.ContainsKey("pack"))
            {
                print(resp["pack"].ToString() + "frome : " + json);
                packList = DeserializeJson<List<pack>>(resp["pack"].ToString());
                packList.ToString();
                phasesRules.Add(resp["id"].ToString(), packList);
            }
            phases.Add(resp["name"].ToString(), resp["id"].ToString());

        }
        print(stringList);
    }

    public void clearContent()
    {
        foreach (Transform c in objList.transform.GetChildren())
        {
            Destroy(c.gameObject);
        }
        selectedOp.Clear();
        LastToggle = null;
        opKeyList.Clear();
        originalRules = rulesTxt.GetComponent<TextMeshProUGUI>().text;
        togList.Clear();

    }

    public Dictionary<string, List<pack>> getPackList()
    {
        return(phasesRules);
    }

    public List<pack> getPhasesPackList(string id)
    {
        return (phasesRules[id]);
    }
    public void setSelection()
    {
        string strRules = null;
        clearContent();
        parseRulesOpText();
        foreach (string elem in stringList)
        {
            foreach (string str in opKeyList)
            {
                GameObject t = Instantiate(m_toggle) as GameObject;
                Toggle toggle = t.GetComponent<Toggle>();
                toggle.isOn = false;
                t.GetComponentInChildren<Text>().text = elem;

                toggle.onValueChanged.AddListener(delegate
                {
                    string togString = toggle.GetComponentInChildren<Text>().text;
                    if (toggle.isOn)
                    {
                        addRulesOpItem(togString);
                        strRules = updateRulesTextForOp();
                    }
                    else
                    {
                        toSend.RemoveAll(x => x.Contains(togString));
                    }
                });
                t.transform.SetParent(objList.transform, false);
                togList.Add(t);
            }
        }


    }

    public void setProjectId(int id)//!\DEPRECATED DO NOT USE IT EXEPT IF API GET BACK TO PREVIOUS VERSION
    {
        projectId = id;
        ModelRessource modelRes = GameObject.Find("ModelRessource").GetComponent<ModelRessource>();

        modelRes.getAll(projectId.ToString(), applyinResponse);
    }

    public void setProjectName(string name)
    {
        projectName = name;
        ModelPhases modelRes = GameObject.Find("ModelPhases").GetComponent<ModelPhases>();

        modelRes.getAll(projectName, applyinResponse);
    }

    public Dictionary<string, string> getSelectedList()
    {
        Dictionary<string, string> toReturn = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> sel in selectedOp)
        {
            toReturn.Add(sel.Key, phases[sel.Value]);
        }
        return (toReturn);
    }

    public void setSelectedList(List<string> l)
    {
        toSend = l;
    }
}

