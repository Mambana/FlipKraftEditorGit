using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonLogicapOperator : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
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

        while ((lastIdx = rulesString.IndexOf("$ol", lastIdx)) != -1)
        {
            if (lastIdx >= 0)
            {
                key = rulesString.Substring(lastIdx, 6);
                lastIdx += 6;
                if (!opKeyList.Contains(key))
                    opKeyList.Add(key);
            }

        }
    }

    public void addRulesOpItem(string toAdd)
    {
        foreach (string item in opKeyList)
        {
            if (!selectedOp.ContainsKey(item))
            {
                print("created :" + item);
                selectedOp.Add(item, toAdd);
                return;
            }
        }

        string lostKey = selectedOp.First().Key;

        selectedOp.Remove(lostKey);
        selectedOp.Add(lostKey, toAdd);

    }


    public void updateRulesTextForOp()
    {
        string rulesString = "";
        foreach (KeyValuePair<string, string> op in selectedOp)
        {
            rulesString = originalRules.Replace(op.Key, op.Value);
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

                if (toSend.Where(x => x.Contains(elem)).FirstOrDefault() == null)
                    toggle.isOn = false;
                t.GetComponentInChildren<Text>().text = elem;

                toggle.onValueChanged.AddListener(delegate
                {
                    string togString = toggle.GetComponentInChildren<Text>().text;
                    if (toggle.isOn)
                    {
                        if (toSend.Where(x => x.Contains(togString)).FirstOrDefault() == null)
                            toSend.Add(togString);
                        addRulesOpItem(togString);
                        updateRulesTextForOp();
                        if (LastToggle && LastToggle != toggle)
                            LastToggle.isOn = false;
                        LastToggle = toggle;
                    }
                    else
                    {
                        toSend.RemoveAll(x => x.Contains(togString));
                    }
                });
                t.transform.SetParent(objList.transform, false);
            }
        }


    }
    public List<string> getSelectedList()
    {
        return (toSend);
    }
}
