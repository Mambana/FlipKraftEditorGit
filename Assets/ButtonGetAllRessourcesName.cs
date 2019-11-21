using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class ButtonGetAllRessourcesName : MonoBehaviour
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


    private List<GameObject> togList;
    private Dictionary<string, string> id_list;
    private Dictionary<string, string> selectedOp;
    private List<string> opKeyList;
    private int maxOpKey = 0;
    Toggle LastToggle;
    string originalRules;

    string parser;
    int ofset;
    int activeToggle = 0;
    void Start()
    {
        id_list = new Dictionary<string, string>();
        togList = new List<GameObject>();
        toSend = new List<string>();
        stringList = new List<string>();
        selectedOp = new Dictionary<string, string>();
        LastToggle = null;
        opKeyList = new List<string>();
        if (playerRessources)
        {
            parser = "$p";
            ofset = 5;
        }
        else
        {
            parser = "$r";
            ofset = 5;
        }
       
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
       foreach(GameObject tog in togList)
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
        print(json);
        Dictionary<int, Dictionary<string, string>> allRessource = new Dictionary<int, Dictionary<string, string>>();
        List<object> respList = DeserializeJson<List<object>>(json);
     
      
        //   destroyList(ressources);
        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            if (playerRessources)
            {

                if (!resp["player_value"].ToString().Equals("0") && !resp["player_value"].ToString().Equals(""))
                {
                    stringList.Add(resp["name"].ToString());
                    id_list.Add(resp["name"].ToString(), resp["id"].ToString());
                }

            }
            else
            {            
                  stringList.Add(resp["name"].ToString());
                  id_list.Add(resp["name"].ToString(), resp["id"].ToString());
            }
          
        }
        print(stringList);
    }

    public Dictionary<string, string> getSelectedList()
    {
        Dictionary<string, string> toReturn = new Dictionary<string, string>();
        foreach(KeyValuePair<string, string> sel in selectedOp)
        {
            toReturn.Add(sel.Key, id_list[sel.Value]);
        }
        return (toReturn);
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

    public void setProjectName(string  name)
    {
        projectName = name;
        ModelRessource modelRes = GameObject.Find("ModelRessource").GetComponent<ModelRessource>();

        modelRes.getAll(projectName, applyinResponse);
    }


    public void setSelectedList(List<string> l)
    {
        toSend = l;
    }
}
