using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
public class ButtonPlayerNumber : MonoBehaviour
{
    [SerializeField]
    GameObject objList;
    [SerializeField]
    string[] stringList;
    [SerializeField]
    GameObject m_toggle;
    [SerializeField]
    GameObject rulesTxt;
    private List<string> toSend;
    private Dictionary<string, string> selectedOp;
    private List<string> opKeyList;
    private int maxOpKey = 0;
    Toggle LastToggle;
    string originalRules;
    private List<GameObject> togList;
    // Start is called before the first frame update
    void Start()
    {
        togList = new List<GameObject>();
        toSend = new List<string>();
        selectedOp = new Dictionary<string, string>();
        LastToggle = null;
        opKeyList = new List<string>();

    }

    // Update is called once per frame
    void Update()
    {

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

    public void setSelectedList(List<string> selection)
    {
        toSend = selection;
    }

    private void parseRulesOpText()
    {
        string rulesString = rulesTxt.GetComponent<TextMeshProUGUI>().text;
        string key;
        int lastIdx = 0;

        while ((lastIdx = rulesString.IndexOf("$j", lastIdx)) != -1)
        {
            if (lastIdx >= 0)
            {
                key = rulesString.Substring(lastIdx, 5);
                lastIdx += 5;
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
      //  updateRulesTextForOp();

    }

    public Dictionary<string, string> getSelectedList()
    {
        return (selectedOp);
    }
    public void updateRulesTextForOp()
    {
        string rulesString = originalRules;
        int i = 0;
        foreach (KeyValuePair<string, string> op in selectedOp)
        {
         //   print(i.ToString() +":"+ op.Key + " " +op.Value);
           // i++;
            rulesString = rulesString.Replace(op.Key, op.Value);
        }
        rulesTxt.GetComponent<TextMeshProUGUI>().text = rulesString;
    }
    public void setSelection()
    {
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
                        updateRulesTextForOp();
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

}
