using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class ModifyVisualCardController : BasicController
{
    int projectId;
    int cardId;
    string projectName;
    
    ImageHandler imageHandler;

    [SerializeField]
    GameObject trash;

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
   
    List<object> lastAssoc;
    void Start()
    {
       
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

    public void setRessourceOnCardSprite(string json, GameObject obj)
    {
   
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        ImageHandler imgHandlerScr = imageHandler.GetComponent<ImageHandler>();
    
        if (obj != null)
            obj.GetComponent<Image>().sprite = imgHandlerScr.GetSprite(int.Parse(resp["img_id"].ToString()));
        
    }

    public void applyOnCard(string json)
    {
        print(json);
        GameObject modelRessource = GameObject.Find("ModelRessource");
        ModelRessource modelResScr = modelRessource.GetComponent<ModelRessource>();
        Dictionary<string, string> cardData = new Dictionary<string, string>();
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        cardData.Add("name", resp["name"].ToString());
        cardData.Add("description", resp["description"].ToString());
        cardData.Add("fk_id_project", resp["fk_id_project"].ToString());
        List<object>assocData = DeserializeJson<List<object>>(resp["associations"].ToString());
        inputName.GetComponent<TMP_InputField>().text = cardData["name"];
        inputDesc.GetComponent<TMP_InputField>().text = cardData["description"];
        Dictionary<int, Dictionary<string, string>> assocList = new Dictionary<int, Dictionary<string, string>>();
        int i = 0;
        foreach (object obj in assocData)
        {
            Dictionary<string, object> assocResp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> assoc = new Dictionary<string, string>();
           
            assoc.Add("idr", assocResp["idr"].ToString());
            assoc.Add("value", assocResp["value"].ToString());
            assoc.Add("posX", assocResp["posX"].ToString());
            assoc.Add("posY", assocResp["posY"].ToString());
           
            assocList.Add(i, assoc);
            i++;
        }
        foreach (KeyValuePair<int, Dictionary<string, string>> assoc in assocList)
        {
            GameObject dragable = Instantiate(dragRessource) as GameObject;
            DragAndDrop dragableScr = dragable.GetComponent<DragAndDrop>();
            dragableScr.setAssocId(int.Parse(assoc.Value["idr"]));
            dragableScr.setCardId(cardId);
            dragableScr.setRessourceId(int.Parse(assoc.Value["idr"]));
            dragableScr.setProjectId(projectId);
            dragableScr.setProjectName(projectName);
            dragableScr.setTrash(trash);
            dragableScr.setValue(int.Parse(assoc.Value["value"]));
            dragableScr.setLinked(true);
            dragable.transform.SetParent(cardVisual.transform, false);
            modelResScr.find(int.Parse(assoc.Value["idr"]), projectName, null ,setRessourceOnCardSprite, dragable);
            dragableScr.setPosition(float.Parse(assoc.Value["posX"]), float.Parse(assoc.Value["posY"]));

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
            scr.setTrash(trash);
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

        projectId = int.Parse(args["project_id"]);
        cardId = int.Parse(args["id"]);
        projectName = args["project_name"];
        modelRessource = GameObject.Find("ModelRessource");
        modelCard = GameObject.Find("ModelCard");
        ModelRessource modelResScr = modelRessource.GetComponent<ModelRessource>();
        ModelCard modelCardScr = modelCard.GetComponent<ModelCard>();
        modelCardScr.find(cardId, projectName, applyOnCard);
     
        modelResScr.getAll(projectName, applyInRessource);
    
       
        ConfirmVisualCard butScr = confirmButton.GetComponent<ConfirmVisualCard>();
        butScr.setIdToModify(cardId);
        butScr.setProjectId(projectId.ToString());
        butScr.setProjectName(projectName);
        RemoveCardButton rmScr = removeButton.GetComponent<RemoveCardButton>();
        rmScr.setIdToRemove(cardId);
        rmScr.setProjectId(projectId.ToString());
        rmScr.setProjectName(projectName);
 
        createRuleButton.GetComponent<CreateCardButton>().setIdToModify(cardId);
        createRuleButton.GetComponent<CreateCardButton>().setProjectId(projectId);
        createRuleButton.GetComponent<CreateCardButton>().setProjectName(projectName);
        RulesListButton.GetComponent<CreateCardButton>().setIdToModify(cardId);
        RulesListButton.GetComponent<CreateCardButton>().setProjectId(projectId);
        RulesListButton.GetComponent<CreateCardButton>().setProjectName(projectName);

    }

  
}
