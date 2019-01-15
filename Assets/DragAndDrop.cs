using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private List<GameObject> objectList;
    private bool dragged;
    private string name;
    private GameObject card;
    private GameObject trash;
    Vector2 cardSize;
    Vector2 ressSize;
    // Start is called before the first frame update
    void Start()
    {
        objectList = new List<GameObject>();
        card = GameObject.Find("CardVisual");
        trash = GameObject.Find("Trash");
        objectList.Add(card);
        objectList.Add(trash);
        dragged = false;
        name = "";
        cardSize = card.GetComponent<RectTransform>().sizeDelta;
        ressSize = gameObject.GetComponent<RectTransform>().sizeDelta;
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
                gameObject.transform.position.y - ressSize.y >= obj.transform.position.y - objSize.y))
            {
                print(obj);
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
        }
        else if (overObj == trash)
            Destroy(gameObject);
        else
            gameObject.transform.position = new Vector3(Input.mousePosition.x - ressSize.x / 2, Input.mousePosition.y + ressSize.y / 2, 1);
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
}
