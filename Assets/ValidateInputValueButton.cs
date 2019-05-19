using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ValidateInputValueButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject inputValue;

    private GameObject model;
    private GameObject valueText;
    private string projectId;
    private string cardId;
    private string ressourceId;
    private string posX;
    private string posY;
    private string assocId;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        model = GameObject.Find("ModelAssociation");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void click()
    {
        string value = inputValue.GetComponent<TMP_InputField>().text;
        ModelAssociation modelScr = model.GetComponent<ModelAssociation>();
        if (assocId == null)
            modelScr.addCollections(value, projectId, cardId, ressourceId, 
                posX,
                posY);
        else
            modelScr.updateField(assocId, value, projectId, cardId, ressourceId,
               posX,
               posY);
        valueText.GetComponent<TextMeshProUGUI>().text = value;
        Destroy(inputValue);
    }

    public void prepareAction(string pId, string cId, string rId, string pX, string pY, GameObject vText ,string aId = null)
    {
        projectId = pId;
        cardId = cId;
        ressourceId = rId;
        posX = pX;
        posY = pY;
        assocId = aId;
        valueText = vText;
    }
}
