using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject inputEmail;
    [SerializeField]
    private GameObject inputPswd;
    
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

        data.updateData("email", inputEmail.GetComponent<TMP_InputField>().text);
        print(data.access("email"));
        but.SendToDispatch();
    }
}
