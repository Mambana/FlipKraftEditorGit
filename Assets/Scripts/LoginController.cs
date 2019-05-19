using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoginController : BasicController
{
    [SerializeField]
    GameObject uriInput;
    // Start is called before the first frame update
    void Start()
    {
        // http://test.flipkraft.ovh/
        // http://flipkraft.ovh/

        string uri = Application.absoluteURL;
        if (uri.Equals(""))
            uri ="http://test.flipkraft.ovh/";

        int slashIdx = uri.IndexOf('/');

        if (slashIdx == -1)
            {
                uriInput.GetComponent<TMP_InputField>().text = uri;
                return;
            }
        slashIdx = uri.IndexOf('/', slashIdx + 1);
        slashIdx = uri.IndexOf('/', slashIdx + 1);
        if (slashIdx == -1)
        {
            uriInput.GetComponent<TMP_InputField>().text = uri;
            return;
        }
        string apiUri = uri.Substring(0, slashIdx);
        uriInput.GetComponent<TMP_InputField>().text = apiUri;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
