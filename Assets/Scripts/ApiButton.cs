using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ApiButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void click()
    {
        HttpRequest www = gameObject.GetComponent<HttpRequest>();
        //Dictionary<string, string> test = new Dictionary<string, string>();

        //test["testKey"] = "testValue";
        www.Get("https://ptsv2.com/t/b2jqq-1547487986/post", Debug.Log);
    }
}
