using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;


/*[{id:1,vars:[1,3, '+', '-', 3]},{id:2,vars:[1, '>', 3]}]*/
public class buttonSetRulesList : MonoBehaviour
{
    [SerializeField]
    private GameObject rulesSetText;
    private List<GameObject> toggleList;
    [SerializeField]
    GameObject objList;
    [SerializeField]
    string[] stringList;
    Dictionary<string, string> rulesSetList;
    [SerializeField]
    GameObject m_toggle;
    private List<string> toSend;
    private string selectedOne;

    // Start is called before the first frame update
    void Start()
    {
        rulesSetList = new Dictionary<string, string>();
        rulesSetList.Add("playFirstCardFromPhase", "Play the first card from phase number :p1");
        rulesSetList.Add("playAnyCardFromPhase", "Play any card from phase number :p1");
        rulesSetList.Add("evaluateCardDuel", "if player 1 have the strongest ressource  beetween $r1-2 and $r2-3 do to $j1-0 : $p3-6 $o1-4 $v1-5 " +
            "if player 2 have the strongest ressource beetween $r1-2 and $r2-3 do to $j2-1 : $p3-6 $o1-4 $v1-5" +
            "");
        rulesSetList.Add("determineWinner", "if player 1 's ressources is $ol-0 $r1-1 than the player 2, then player 1 win other wise," +
            "the player 2 win ");
        toSend = new List<string>();
        toggleList = new List<GameObject>();
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
    }

    public void setSelectedList(List<string> selection)
    {
        toSend = selection;
    }

    private void unCheckAll(string togstring)
    {
        string itemString;
        foreach(GameObject item in toggleList)
        {
                itemString = item.GetComponentInChildren<Text>().text;
            if (itemString.Equals(togstring) == false)
                item.GetComponent<Toggle>().isOn = false;
        }
    }
    public void setSelection()
    {
        clearContent();
        toggleList.Clear();
        foreach (string elem in stringList)
        {
            GameObject t = Instantiate(m_toggle) as GameObject;
            Toggle toggle = t.GetComponent<Toggle>();

            if (toSend.Where(x => x.Contains(elem)).FirstOrDefault() == null)
                toggle.isOn = false;
            if (elem.Equals(selectedOne))
                toggle.isOn = true;
            t.GetComponentInChildren<Text>().text = elem;

            toggle.onValueChanged.AddListener(delegate
            {
                string togString = toggle.GetComponentInChildren<Text>().text;
                if (toggle.isOn)
                {
                    unCheckAll(togString);      
                    selectedOne = togString;
                    print(selectedOne);
                    rulesSetText.GetComponent<TextMeshProUGUI>().text = rulesSetList[selectedOne];
                }
                else
                    toSend.RemoveAll(x => x.Contains(togString));
            });
            t.transform.SetParent(objList.transform, false);
            toggleList.Add(t);
        }


    }
    public List<string> getSelectedList()
    {
        return (toSend);
    }
}
