using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OverviewController : BasicController
{
    [SerializeField]
    GameObject Model;
    int id;
    [SerializeField]
    GameObject projName;
    [SerializeField]
    GameObject min;
    [SerializeField]
    GameObject max;
    [SerializeField]
    GameObject desc;
    [SerializeField]
    GameObject nb_cards;
    [SerializeField]
    GameObject nb_ressources;
    [SerializeField]
    GameObject projNameTitle;
    [SerializeField]
    GameObject modifyButton;
    [SerializeField]
    GameObject ressourceButton;
    [SerializeField]
    GameObject cardButton;
    // Use this for initialization
    void Start () {
     


    }

    // Update is called once per frame
    void Update () {
		
	}

    public override void apply()
    {
       
        id = int.Parse(args["id"]);
        
        Model = GameObject.Find("Model");
        modifyButton.GetComponent<ModifyButton>().setIdToModify(id);
        ressourceButton.GetComponent<RessourceListButton>().setCurrentProjectId(id);
        cardButton.GetComponent<CardListButton>().setCurrentProjectId(id);
        ModelTest ModelScript = Model.GetComponent<ModelTest>();
        projNameTitle.GetComponent<TextMeshProUGUI>().text = ModelScript.find(id, "name");
        projName.GetComponent<TextMeshProUGUI>().text += " "+ ModelScript.find(id, "name");
        min.GetComponent<TextMeshProUGUI>().text += " " + ModelScript.find(id, "min");
        max.GetComponent<TextMeshProUGUI>().text += " " + ModelScript.find(id, "max");
        desc.GetComponent<TextMeshProUGUI>().text += " " + ModelScript.find(id, "description");
        nb_cards.GetComponent<TextMeshProUGUI>().text += " " + ModelScript.find(id, "nb_cards");
        nb_ressources.GetComponent<TextMeshProUGUI>().text += " " + ModelScript.find(id, "nb_ressources");
    }

    
}
