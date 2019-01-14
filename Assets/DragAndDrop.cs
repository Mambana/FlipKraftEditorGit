using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool dragged;
    private string name;
    private GameObject card;
    Vector2 cardSize;
    Vector2 ressSize;
    // Start is called before the first frame update
    void Start()
    {
        card = GameObject.Find("CardVisual");
        dragged = false;
        name = "";
        cardSize = card.GetComponent<RectTransform>().sizeDelta; ;
        ressSize = gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragged)
        gameObject.transform.position = new Vector3(Input.mousePosition.x - ressSize.x / 2, Input.mousePosition.y + ressSize.y / 2, 1);
    }

  public void onDrag()
    {
        dragged = true;
        gameObject.transform.position = new Vector3(Input.mousePosition.x - ressSize.x / 2 , Input.mousePosition.y + ressSize.y / 2, 1);
    
    }

    public void onDrop()
    {
        if (gameObject.transform.position.x + ressSize.x > card.transform.position.x + cardSize.x)
            gameObject.transform.position = new Vector2(card.transform.position.x + cardSize.x - ressSize.x, gameObject.transform.position.y);
        if (gameObject.transform.position.y > card.transform.position.y)
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, card.transform.position.y);
        if (gameObject.transform.position.x < card.transform.position.x)
            gameObject.transform.position = new Vector2(card.transform.position.x, gameObject.transform.position.y);
        if (gameObject.transform.position.y - ressSize.y < card.transform.position.y - cardSize.y)
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, card.transform.position.y - cardSize.y + ressSize.y);
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
