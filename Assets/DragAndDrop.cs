using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool dragged;
    // Start is called before the first frame update
    void Start()
    {
        dragged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragged)
        gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
    }

  public void onDrag()
    {
        dragged = true;
        gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
    
    }

    public void onDrop()
    {
        dragged = false;
    }
}
