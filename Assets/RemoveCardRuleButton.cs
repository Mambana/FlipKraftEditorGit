using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveCardRuleButton : MonoBehaviour
{
    // Start is called before the first frame update
    int id;
    int projectId;
    int cardId;
    GameObject model;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCardId(int id)
    {
        cardId = id;
    }

    public void setIdToRemove(int _id)
    {
        id = _id;
    }

    public void setProjectId(int id)
    {
        projectId = id;
    }

    void callDispatcher(string json)
    {
        print("call");
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("project_id", projectId.ToString());
        but.addParam("id", cardId.ToString());
        but.addParam("card_id", cardId.ToString());
        but.SendToDispatch();
    }

    void click()
    {
        model = GameObject.Find("ModelCardRules");
        ModelCardRules modelScr = model.GetComponent<ModelCardRules>();
        modelScr.removeElem(id, cardId, callDispatcher);
    }

}
