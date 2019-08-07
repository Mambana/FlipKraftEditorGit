using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModifyCardRulesController : BasicController
{
    // Use this for initialization
    // Use this for initialization
    GameObject model;
    [SerializeField]
    GameObject confirmButton;
    [SerializeField]
    GameObject removeButton;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputDesc;
    [SerializeField]
    GameObject signalsBut;
    [SerializeField]
    GameObject typesBut;
    [SerializeField]
    GameObject instructionsBut;
    [SerializeField]
    GameObject ressourcesBut;

    int projectId;
    int id;
    int cardId;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public List<string> createListFromJson(string json)
    {
        List<string> l = new List<string>();
        List<object> serial = DeserializeJson<List<object>>(json);
        foreach (object obj in serial)
        {
            l.Add(obj.ToString());
        }
        return (l);
    }

    public void applyInServerResponse(string json)
    {
        print(json);
        Dictionary<string, string> ressourceData = new Dictionary<string, string>();
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        ressourceData.Add("name", resp["name"].ToString());
        ressourceData.Add("description", resp["description"].ToString());
        inputName.GetComponent<TMP_InputField>().text = ressourceData["name"];
        inputDesc.GetComponent<TMP_InputField>().text = ressourceData["description"];
        signalsBut.GetComponent<ButtonSetArraySelection>().setSelectedList(createListFromJson(resp["signals"].ToString()));
        typesBut.GetComponent<ButtonSetArraySelection>().setSelectedList(createListFromJson(resp["var_type"].ToString()));
        instructionsBut.GetComponent<ButtonSetArraySelection>().setSelectedList(createListFromJson(resp["instructions"].ToString()));
        ressourcesBut.GetComponent<ButtonGetAllRessourcesName>().setSelectedList(createListFromJson(resp["variables"].ToString()));
        ressourcesBut.GetComponent<ButtonGetAllRessourcesName>().setProjectId(projectId);
    }

    public override void apply()
    {
        cardId = int.Parse(args["card_id"]);
        projectId = int.Parse(args["project_id"]);
        id = int.Parse(args["rule_id"]);
        model = GameObject.Find("ModelCardRules");
        ModelCardRules modelScr = model.GetComponent<ModelCardRules>();
        modelScr.find(id, cardId, applyInServerResponse);
        ConfirmCardRuleModificationButton butScr = confirmButton.GetComponent<ConfirmCardRuleModificationButton>();
        butScr.setIdToModify(id);
        butScr.setProjectId(int.Parse(args["project_id"]));
        butScr.setCardId(cardId);
        RemoveCardRuleButton rmButScr = removeButton.GetComponent<RemoveCardRuleButton>();
        rmButScr.setProjectId(int.Parse(args["project_id"]));
        rmButScr.setIdToRemove(id);
        rmButScr.setCardId(cardId);
        
    }
}
