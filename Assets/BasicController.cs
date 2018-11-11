using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour {

    protected Dictionary<string, string> args;
    // Use this for initialization
    void Start () {
		
	}
    public void setParams(Dictionary<string, string> l)
    {
        if (args != null)
            args.Clear();
        args = l;
    }

    virtual public void apply()
    {

    }
    // Update is called once per frame
    void Update () {
		
	}
}
