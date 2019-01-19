using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonCreateDraggleRess : MonoBehaviour
{

    [SerializeField]
    private GameObject DraggableElem;
    private GameObject card;
    private int projectId;
    private int cardId;
    private int ressId;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        card = GameObject.Find("CardVisual");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void click()
    {
        GameObject elem = Instantiate(DraggableElem);
        DragAndDrop scr = elem.GetComponent<DragAndDrop>();
        string ressName = gameObject.transform.Find("RessourceName").GetComponent<TextMeshProUGUI>().text;

        scr.setName(ressName);
        scr.setCardId(cardId);
        scr.setProjectId(projectId);
        scr.setRessourceId(ressId);
        elem.transform.position = gameObject.transform.localPosition;
        elem.transform.SetParent(card.transform, false);
    }

    public void setProjectId(int id)
    {
        projectId = id;
    }

    public void setCardId(int id)
    {
        cardId = id;
    }

    public void setRessId(int id)
    {
        ressId = id;
    }
}
