using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCreateDraggleRess : MonoBehaviour
{

    [SerializeField]
    private GameObject DraggableElem;
    private GameObject card;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        card = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void click()
    {
        GameObject elem = Instantiate(DraggableElem);

        elem.transform.position = gameObject.transform.localPosition;
        elem.transform.SetParent(card.transform, false);
    }
}
