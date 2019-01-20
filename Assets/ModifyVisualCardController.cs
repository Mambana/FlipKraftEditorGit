using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModifyVisualCardController : BasicController
{
    int projectId;

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
    GameObject dragRessource;

    [SerializeField]
    GameObject cardVisual;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void apply()
    {
        GameObject modelRessource;
        GameObject modelCard;

        projectId = int.Parse(args["project_id"]);
        int cardId = int.Parse(args["id"]);
        modelRessource = GameObject.Find("ModelRessource");
        modelCard = GameObject.Find("ModelCard");
        ModelRessource modelResScr = modelRessource.GetComponent<ModelRessource>();
        ModelCard modelCardScr = modelCard.GetComponent<ModelCard>();
        Dictionary<string, string> cardData = modelCardScr.find(cardId);
        Dictionary<int, Dictionary<string, string>> assocData = modelCardScr.getLastAssoc();
        inputName.GetComponent<TMP_InputField>().text = cardData["name"];
        inputDesc.GetComponent<TMP_InputField>().text = cardData["description"];
        Dictionary<int, Dictionary<string, string>> allRes = modelResScr.getAll(projectId.ToString());
        foreach (KeyValuePair<int, Dictionary<string, string>> res in allRes)
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
        foreach(KeyValuePair<int, Dictionary<string, string>> assoc in assocData)
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
        ConfirmVisualCard butScr = confirmButton.GetComponent<ConfirmVisualCard>();
        butScr.setIdToModify(cardId);
        butScr.setProjectId(projectId.ToString());
    }
}
