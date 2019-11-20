using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class ModifyCardController : BasicController {

    int projectId;
    int cardId;
    string projectName;

    ImageHandler imageHandler;

    [SerializeField]
    GameObject elemInList;

    [SerializeField]
    GameObject listOfProj;

    [SerializeField]
    GameObject inputName;

    [SerializeField]
    GameObject inputDesc;

    [SerializeField]
    GameObject confirmButton;

    [SerializeField]
    GameObject removeButton;

    [SerializeField]
    GameObject dragRessource;

    [SerializeField]
    GameObject cardVisual;

    [SerializeField]
    GameObject imgHandler;

    [SerializeField]
    GameObject createRuleButton;

    [SerializeField]
    GameObject RulesListButton;
    // Start is called before the first frame update

    [SerializeField]
    GameObject PhasesDropDown;


    Dictionary<string, string> phases;
    List<object> lastAssoc;
    void Start()
    {
        phases = new Dictionary<string, string>();
        //listOfProj = GameObject.FindWithTag("Container");
        imageHandler = GameObject.Find("ImageHandler").GetComponent<ImageHandler>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public string getSelectedPhases()
    {
        int idx = PhasesDropDown.GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuOptions = PhasesDropDown.GetComponent<Dropdown>().options;
        return (phases[menuOptions[idx].text]);
    }
    public void applyForPhases(string json)
    {
        Dictionary<int, Dictionary<string, string>> allRessource = new Dictionary<int, Dictionary<string, string>>();
        List<object> respList = DeserializeJson<List<object>>(json);
        Dropdown dropdownEventIDs = PhasesDropDown.GetComponent<Dropdown>();
        int i = 0;

        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> ressourceData = new Dictionary<string, string>();

            ressourceData.Add("name", resp["name"].ToString());
            ressourceData.Add("id", resp["id"].ToString());
            allRessource.Add(i, ressourceData);
            i++;
        }

        foreach(KeyValuePair<int, Dictionary<string, string>> res in allRessource)
        {
            phases.Add(res.Value["name"], res.Value["id"]);
        }
        foreach(KeyValuePair<string, string> p in phases)
        {
            dropdownEventIDs.AddOptions(new List<string> { p.Key });
        }
    }
    public void applyInRessource(string json)
    {
        Dictionary<int, Dictionary<string, string>> allRessource = new Dictionary<int, Dictionary<string, string>>();
        List<object> respList = DeserializeJson<List<object>>(json);
        int i = 0;

        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> ressourceData = new Dictionary<string, string>();

            ressourceData.Add("name", resp["name"].ToString());
            ressourceData.Add("description", resp["description"].ToString());
            ressourceData.Add("id", resp["id"].ToString());
            ressourceData.Add("fk_id_project", resp["fk_id_project"].ToString());
            ressourceData.Add("img_id", resp["img_id"].ToString());
            allRessource.Add(i, ressourceData);
            i++;
        }
        foreach (KeyValuePair<int, Dictionary<string, string>> res in allRessource)
        {
            GameObject toAdd = Instantiate(elemInList) as GameObject;
            ButtonCreateDraggleRess scr = toAdd.GetComponent<ButtonCreateDraggleRess>();
            scr.setProjectId(projectId);
            scr.setCardId(cardId);
            scr.setImgId(int.Parse(res.Value["img_id"]));
            scr.setRessId(int.Parse(res.Value["id"]));
            //  toAdd.GetComponent<Image>().sprite = imageHandler.GetSprite(int.Parse(res.Value["id"]));
            toAdd.transform.Find("RessourceName").GetComponent<TextMeshProUGUI>().text = res.Value["name"];
            toAdd.transform.Find("RessourceDesc").GetComponent<TextMeshProUGUI>().text = res.Value["description"];
            toAdd.GetComponent<Image>().sprite = imageHandler.GetSprite(int.Parse(res.Value["img_id"]));
            toAdd.transform.SetParent(listOfProj.transform, false);
        }
    }



    public override void apply()
    {
        GameObject modelRessource;
        GameObject modelCard;
        GameObject modelPhases;

        projectId = int.Parse(args["project_id"]);
        projectName = args["project_name"];
        modelRessource = GameObject.Find("ModelRessource");
        modelCard = GameObject.Find("ModelCard");
        modelPhases = GameObject.Find("ModelPhases");
        ModelRessource modelResScr = modelRessource.GetComponent<ModelRessource>();

        modelResScr.getAll(projectName, applyInRessource);
        modelPhases.GetComponent<ModelPhases>().getAll(projectName, applyForPhases);

        ConfirmVisualCard butScr = confirmButton.GetComponent<ConfirmVisualCard>();
        butScr.setIdToModify(-1);
        butScr.setProjectId(projectId.ToString());
        butScr.setProjectName(projectName);
        CancelRessourceCreation rmScr = removeButton.GetComponent<CancelRessourceCreation>();
 
        rmScr.setProjectId(projectId.ToString());
        rmScr.setProjectName(projectName);

        /*createRuleButton.GetComponent<CreateCardButton>().setIdToModify(cardId);
        createRuleButton.GetComponent<CreateCardButton>().setProjectId(projectId);
        RulesListButton.GetComponent<CreateCardButton>().setIdToModify(cardId);
        RulesListButton.GetComponent<CreateCardButton>().setProjectId(projectId);*/

    }

}
