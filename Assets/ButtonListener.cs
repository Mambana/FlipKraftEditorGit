using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonListener : MonoBehaviour {

    [SerializeField]
    private string UiNameToCall;
    [SerializeField]
    private string UiScriptToCall;
    [SerializeField]
    private Dictionary<string, string> param = null;
   
    private GameObject Dispatcher;
	// Use this for initialization
	void Start () {
        Dispatcher = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void addParam(string key, string ToAdd)
    {
        if (param == null)
            param = new Dictionary<string, string>();
        if (!param.ContainsKey(ToAdd))
            param.Add(key, ToAdd);
        else
            param[key] = ToAdd;
    }

    public void setParams(Dictionary<string,string> l)
    {
        param = l;
    }

    public Dictionary<string, string> getParam()
    {
        return (param);
    }

    public void SendToDispatch()
    {

        
        Dispatcher.GetComponent<Dispatcher>().dispatch(UiNameToCall, UiScriptToCall, param);
    }

    public bool hasKey(string key)
    {
        return (param.ContainsKey(key));
    }
}
