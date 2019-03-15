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
        string uri = Application.absoluteURL;
        int slashIdx = uri.IndexOf('/');
        if (slashIdx == -1)
            {
                uriInput.GetComponent<TMP_InputField>().text = uri;
                return;
            }
        string uriPart = uri.Substring(0, slashIdx);
        int lastIdx = uriPart.IndexOf('/');
        if (lastIdx == -1)
        {
            uriInput.GetComponent<TMP_InputField>().text = uri;
            return;
        }
        string apiUri = uri.Substring(slashIdx + 2, lastIdx);
        uriInput.GetComponent<TMP_InputField>().text = apiUri;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
