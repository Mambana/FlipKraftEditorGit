using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonCreateDraggleRess : MonoBehaviour
{

    [SerializeField]
    private GameObject DraggableElem;
    [SerializeField]
    private GameObject trash;
    private GameObject card;
    private int projectId;
    private int cardId;
    private int ressId;
    private int imgId;
    private int value;
    private ImageHandler imgHandlerScr;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        card = GameObject.Find("CardVisualeEditable");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTrash(GameObject tr)
    {
        trash = tr;
    }
    void click()
    {
        imgHandlerScr = GameObject.Find("ImageHandler").GetComponent<ImageHandler>();
        GameObject elem = Instantiate(DraggableElem);
        DragAndDrop scr = elem.GetComponent<DragAndDrop>();
        string ressName = gameObject.transform.Find("RessourceName").GetComponent<TextMeshProUGUI>().text;

        elem.GetComponent<Image>().sprite = imgHandlerScr.GetSprite(imgId);
        scr.setLinked(false);
        scr.setTrash(trash);
        scr.setName(ressName);
        scr.setCardId(cardId);
        scr.setProjectId(projectId);
        scr.setRessourceId(ressId);

        elem.transform.position = new Vector3(300, 300, 1); 
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

    public void setValue(int val)
    {
        value = val;
    }

    public void setImgId(int id)
    {
        imgId = id;
    }
}
