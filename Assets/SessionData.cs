using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    // Start is called before the first frame update
    Dictionary<string, string> session;

    void Start()
    {
        session = new Dictionary<string, string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateData(string key, string value)
    {
        session[key] = value;
    }

    public string access(string key)
    {
        return (session[key]);
    }
}
