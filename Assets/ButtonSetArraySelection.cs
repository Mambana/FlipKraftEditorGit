using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetArraySelection : MonoBehaviour
{
    [SerializeField]
    GameObject objList;
    [SerializeField]
    string[] stringList;
    [SerializeField]
    GameObject m_toggle;
    private List<string> toSend;
    // Start is called before the first frame update
    void Start()
    {
        toSend = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
 
    public void clearContent()
    {
        foreach(Transform c in objList.transform.GetChildren())
        {       
             Destroy(c.gameObject);
        }
    }

    public void setSelectedList(List<string> selection)
    {
        toSend = selection;
    }

    public void setSelection()
    {
        clearContent();
        foreach (string elem in stringList)
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
                }
                else
                    toSend.RemoveAll(x => x.Contains(togString));                
            });
            t.transform.SetParent(objList.transform, false);           
        }

       
    }
    public List<string> getSelectedList()
    {
        return (toSend);
    }
}
