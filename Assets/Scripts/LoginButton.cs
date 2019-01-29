using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Net;
using System;
using System.Text;

public class LoginButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject inputEmail;
    [SerializeField]
    private GameObject inputPswd;
    [SerializeField]
    private GameObject loginMessage;
    [SerializeField]
    private GameObject apiAddressInput;
    private GameObject session;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        session = GameObject.Find("Session");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void click()
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        SessionData data = session.GetComponent<SessionData>();
        TMP_Text msg = loginMessage.GetComponent<TMP_Text>();
        string email = inputEmail.GetComponent<TMP_InputField>().text;
        string pswd = inputPswd.GetComponent<TMP_InputField>().text;
        string apiAdress = apiAddressInput.GetComponent<TMP_InputField>().text;
      /*  if (email.IndexOf(".") <= 0 || email.IndexOf(".") == email.Length
            || email.IndexOf("@") <= 0 || email.IndexOf("@") == email.Length)
        {
            msg.text = "Please, enter a valide email address";
            return;
        }*/
        if (pswd.Length < 6)
        {
            msg.text = "Please, enter a valide password";
            return;
        }
        data.updateData("email", email);
        data.updateData("id", "1");
        data.updateData("api_address", apiAdress);
        data.updateData("pwd", pswd);
        

        /*var request = (HttpWebRequest)WebRequest.Create(apiAdress);
        string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(email + ":" + pswd));*/

        but.SendToDispatch();
    }
}
