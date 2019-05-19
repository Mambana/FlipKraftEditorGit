using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageSelectionButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject ressourceImage;
    private GameObject confirmButton;

    int id;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        confirmButton = GameObject.Find("ComfirmCreationButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void click()
    {
        ressourceImage.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        ComfirmRessourceCreation butScr = confirmButton.GetComponent<ComfirmRessourceCreation>();
        butScr.setImageId(id);
    }

    public void SetRessourceImage(GameObject ressImage, int imgId)
    {
        ressourceImage = ressImage;
        id = imgId;
    }
}
