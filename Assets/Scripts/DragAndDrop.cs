using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private List<GameObject> objectList;
    private bool dragged;
    private bool linked;
    private int value;
    private string name;
    private GameObject card;
    private GameObject trash;
    private ModelAssociation modelScr;
    private Canvas canvas;
    private GameObject inputValue;
    Vector2 cardSize;
    Vector2 ressSize;
    int projectId;
    int cardId;
    int ressourceId;
    int assocId;

    [SerializeField]
    GameObject inputValuePopPup;
    [SerializeField]
    GameObject valueText;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        modelScr = GameObject.Find("ModelAssociation").GetComponent<ModelAssociation>();
        objectList = new List<GameObject>();
        card = GameObject.Find("CardVisualeEditable");
        trash = GameObject.Find("EditTrash");
        objectList.Add(card);
        objectList.Add(trash);
        dragged = false;
        name = "";
        cardSize = card.GetComponent<RectTransform>().sizeDelta * canvas.scaleFactor;
        ressSize = gameObject.GetComponent<RectTransform>().sizeDelta * canvas.scaleFactor;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dragged)
          gameObject.transform.position = new Vector3(Input.mousePosition.x - ressSize.x / 2, Input.mousePosition.y + ressSize.y / 2, 1);
    }

    public GameObject overedObject()
    {
        Vector2 objSize;
       foreach(GameObject obj in objectList)
        {
           objSize = obj.GetComponent<RectTransform>().sizeDelta;
            if ((gameObject.transform.position.x >= obj.transform.position.x &&
                gameObject.transform.position.x <= obj.transform.position.x + objSize.x ||
                gameObject.transform.position.x + ressSize.x >= obj.transform.position.x &&
                gameObject.transform.position.x + ressSize.x <= obj.transform.position.x + objSize.x) &&
                (gameObject.transform.position.y <= obj.transform.position.y &&
                gameObject.transform.position.y >= obj.transform.position.y - objSize.y ||
                 gameObject.transform.position.y - ressSize.y <= obj.transform.position.y &&
                gameObject.transform.position.y - ressSize.y >= obj.transform.position.y - objSize.y ))
            {
                print("object found");
                return (obj);
            }
        }
        return (null);
    }

  public void onDrag()
    {
        dragged = true;
        gameObject.transform.position = new Vector3(Input.mousePosition.x - ressSize.x / 2 , Input.mousePosition.y + ressSize.y / 2, 1);
        
    }

  public void onDrop()
    {       
        GameObject overObj = overedObject();
        
        if (overObj == card)
        {
            if (gameObject.transform.position.x + ressSize.x > card.transform.position.x + cardSize.x)
                gameObject.transform.position = new Vector2(card.transform.position.x + cardSize.x - ressSize.x, gameObject.transform.position.y);
            if (gameObject.transform.position.y > card.transform.position.y)
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, card.transform.position.y);
            if (gameObject.transform.position.x < card.transform.position.x)
                gameObject.transform.position = new Vector2(card.transform.position.x, gameObject.transform.position.y);
            if (gameObject.transform.position.y - ressSize.y < card.transform.position.y - cardSize.y)
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, card.transform.position.y - cardSize.y + ressSize.y);
            if (linked == false)
            {
                if (inputValue != null)
                    Destroy(inputValue);
                inputValue = Instantiate(inputValuePopPup) as GameObject;
                inputValue.transform.position = new Vector3(-1.5259f, 7.6294f, 1);
                inputValue.transform.SetParent(canvas.transform, false);
                ValidateInputValueButton scr = inputValue.transform.Find("InputValue").Find("Button").GetComponent<ValidateInputValueButton>();
                scr.prepareAction(projectId.ToString(), cardId.ToString(), ressourceId.ToString(),
               gameObject.transform.localPosition.x.ToString(),
               gameObject.transform.localPosition.y.ToString(), valueText);
                    // assocId = modelScr.getNbElement();

                
                dragged = false;
                linked = true;
            }
            else
            {
                if (inputValue != null)
                    Destroy(inputValue);
                inputValue = Instantiate(inputValuePopPup) as GameObject;
                ValidateInputValueButton scr = inputValue.transform.Find("InputValue").Find("Button").GetComponent<ValidateInputValueButton>(); 
                scr.prepareAction(projectId.ToString(), cardId.ToString(), ressourceId.ToString(),
                gameObject.transform.localPosition.x.ToString(),
                gameObject.transform.localPosition.y.ToString(), valueText,assocId.ToString());
                inputValue.transform.position = new Vector3(-1.5259f, 7.6294f, 1);
                inputValue.transform.SetParent(canvas.transform, false);
                dragged = false;
                linked = true;
            }
        }
        else if (overObj == trash)
        {
            print(assocId);
            if (linked)
                modelScr.removeElem(assocId);
            linked = false;
            Destroy(gameObject);
        }
        else
        {
            gameObject.transform.position = new Vector3(Input.mousePosition.x - ressSize.x / 2, Input.mousePosition.y + ressSize.y / 2, 1);
            if (linked)
                modelScr.removeElem(assocId);
            linked = false;
        }
        dragged = false;
    }

    public void setName(string n)
    {
        name = n;
    }

    public string getName()
    {
        return (name);
    }

    public void setProjectId(int id)
    {
        projectId = id;
    }

    public void setCardId(int id)
    {
        cardId = id;
    }

    public void setRessourceId(int id)
    {
        ressourceId = id;
    }

    public void setAssocId(int id)
    {
        assocId = id;
    }

    public void setValue(int newValue)
    {
        value = newValue;
        valueText.GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    public void setPosition(float x, float y)
    {
        gameObject.transform.localPosition = new Vector3(x, y, 1);
    }

    public void setLinked(bool l)
    {
        linked = l;
    }

    public bool getLinked()
    {
        return (linked);
    }
}
