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
    [SerializeField]
    private GameObject errorMsg; 
    private GameObject canvas;
    [SerializeField]
    private GameObject disableScreen;
    private ConfirmVisualCard confirmScr;
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
        confirmScr = GameObject.Find("Validate").GetComponent<ConfirmVisualCard>();
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        model = GameObject.Find("ModelAssociation");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void click()
    {
        int n;
        string value = inputValue.GetComponent<TMP_InputField>().text;
        if (int.TryParse(value, out n) == false)
        {
            errorMsg.GetComponent<TextMeshProUGUI>().text = "Please enter a valide number...";
            return;
        }
        confirmScr.addAssocToModify(projectId, cardId, ressourceId, posX, posY, value, assocId);
        /*ModelAssociation modelScr = model.GetComponent<ModelAssociation>();
        if (assocId == null)
            modelScr.addCollections(value, projectId, cardId, ressourceId, 
                posX,
                posY);
        else
            modelScr.updateField(assocId, value, projectId, cardId, ressourceId,
               posX,
               posY);*/
        valueText.GetComponent<TextMeshProUGUI>().text = value;
        Destroy(transform.parent.parent.gameObject);
        Destroy(disableScreen);
    }

    public void prepareAction(string pId, string cId, string rId, string pX, string pY, GameObject vText ,string aId = null)
    {
        canvas = GameObject.Find("Canvas");
        projectId = pId;
        cardId = cId;
        ressourceId = rId;
        posX = pX;
        posY = pY;
        assocId = aId;
        valueText = vText;
        disableScreen = Instantiate(disableScreen) as GameObject;
        disableScreen.transform.SetParent(canvas.transform, false);
        disableScreen.transform.SetSiblingIndex(98);
        transform.parent.parent.SetSiblingIndex(99);

    }
}
