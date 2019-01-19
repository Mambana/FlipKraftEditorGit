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

        ConfirmVisualCard butScr = confirmButton.GetComponent<ConfirmVisualCard>();
        butScr.setIdToModify(cardId);
        butScr.setProjectId(projectId.ToString());
    }
}
