using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class ModifyVisualCardController : BasicController
{
    int projectId;
    int cardId;

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
    // Start is called before the first frame update
    List<object> lastAssoc;
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

    public void applyOnCard(string json)
    {
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
            assoc.Add("id", assocResp["id"].ToString());
            assoc.Add("fk_id_ressource", assocResp["fk_id_ressource"].ToString());
            assoc.Add("fk_id_cards", assocResp["fk_id_cards"].ToString());
            assoc.Add("fk_id_project", assocResp["fk_id_project"].ToString());
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
            dragableScr.setAssocId(int.Parse(assoc.Value["id"]));
            dragableScr.setCardId(int.Parse(assoc.Value["fk_id_cards"]));
            dragableScr.setRessourceId(int.Parse(assoc.Value["fk_id_ressource"]));
            dragableScr.setProjectId(int.Parse(assoc.Value["fk_id_project"]));
            dragableScr.setValue(int.Parse(assoc.Value["value"]));
            dragableScr.setLinked(true);
            dragable.transform.SetParent(cardVisual.transform, false);
            print(assoc.Value["posX"]);
            print(assoc.Value["posY"]);
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
            allRessource.Add(i, ressourceData);
            i++;
        }
        foreach (KeyValuePair<int, Dictionary<string, string>> res in allRessource)
        {
            GameObject toAdd = Instantiate(elemInList) as GameObject;
            ButtonCreateDraggleRess scr = toAdd.GetComponent<ButtonCreateDraggleRess>();
            scr.setProjectId(projectId);
            scr.setCardId(cardId);
            scr.setRessId(int.Parse(res.Value["id"]));
            toAdd.transform.Find("RessourceName").GetComponent<TextMeshProUGUI>().text = res.Value["name"];
            toAdd.transform.Find("RessourceDesc").GetComponent<TextMeshProUGUI>().text = res.Value["description"];
            toAdd.transform.SetParent(listOfProj.transform, false);
        }
    }

  

    public override void apply()
    {
        GameObject modelRessource;
        GameObject modelCard;

        projectId = int.Parse(args["project_id"]);
        cardId = int.Parse(args["id"]);
        modelRessource = GameObject.Find("ModelRessource");
        modelCard = GameObject.Find("ModelCard");
        ModelRessource modelResScr = modelRessource.GetComponent<ModelRessource>();
        ModelCard modelCardScr = modelCard.GetComponent<ModelCard>();
        modelCardScr.find(cardId, applyOnCard);
     
        modelResScr.getAll(projectId.ToString(), applyInRessource);
    
       
        ConfirmVisualCard butScr = confirmButton.GetComponent<ConfirmVisualCard>();
        butScr.setIdToModify(cardId);
        butScr.setProjectId(projectId.ToString());
        RemoveCardButton rmScr = removeButton.GetComponent<RemoveCardButton>();
        rmScr.setIdToRemove(cardId);
        rmScr.setProjectId(projectId.ToString());
      }
}
