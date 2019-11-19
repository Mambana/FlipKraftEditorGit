using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmErrorBtn : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject paper;
    GameObject parent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDisableScreen(GameObject toSet)
    {
        paper = toSet;
    }

    public void setParent(GameObject toSet)
    {
        parent = toSet;
    }
   public  void click()
    {
        Destroy(paper);
        Destroy(parent);
    }
}
