using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class PlayerValueOrNot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject btnTxt;
    [SerializeField]
    GameObject inputValue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inputValue.activeSelf)
            btnTxt.GetComponent<Text>().text = "No";
        else
            btnTxt.GetComponent<Text>().text = "Yes";
    }

    public void click()
    {
        if (btnTxt.GetComponent<Text>().text.Equals("No"))
        {
            btnTxt.GetComponent<Text>().text = "Yes";
            inputValue.GetComponent<TMP_InputField>().text = "";
            inputValue.SetActive(false);
        }
        else
        {
            btnTxt.GetComponent<Text>().text = "No";
            inputValue.SetActive(true);
        }
    }
}
