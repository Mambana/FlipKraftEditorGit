using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Linq;

public class ButtonGetAllRessourcesName : MonoBehaviour
{
    // Start is called before the first frame update
    private int projectId;
    [SerializeField]
    GameObject objList;
    private List<string> stringList;
    [SerializeField]
    GameObject m_toggle;
    private List<string> toSend;
    void Start()
    {
        toSend = new List<string>();
        stringList = new List<string>();
        ModelRessource modelRes = GameObject.Find("ModelRessource").GetComponent<ModelRessource>();
        
        modelRes.getAll(projectId.ToString(), applyinResponse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void applyinResponse(string json)
    {
        Dictionary<int, Dictionary<string, string>> allRessource = new Dictionary<int, Dictionary<string, string>>();
        List<object> respList = DeserializeJson<List<object>>(json);
     
      
        //   destroyList(ressources);
        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
     
            stringList.Add(resp["name"].ToString());
          
        }

    }

    public void clearContent()
    {
        foreach (Transform c in objList.transform.GetChildren())
        {
            Destroy(c.gameObject);
        }
    }

    public void setSelection()
    {
        clearContent();
        foreach (string elem in stringList)
        {
            print (elem);
            GameObject t = Instantiate(m_toggle) as GameObject;
            Toggle toggle = t.GetComponent<UnityEngine.UI.Toggle>();

            t.GetComponentInChildren<Text>().text = elem;
            toggle.isOn = false;
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

    public void setProjectId(int id)
    {
        projectId = id;
    }

    public List<string> getSelectedList()
    {
        return (toSend);
    }
}
