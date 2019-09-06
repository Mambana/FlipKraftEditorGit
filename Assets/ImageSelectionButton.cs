using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageSelectionButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject ressourceImage;
    [SerializeField]
    int id;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void click()
    {
        var confirmButton = GameObject.Find("ComfirmCreationButton");         
        ressourceImage.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        if (confirmButton)
        {
            ComfirmRessourceCreation butScr = confirmButton.GetComponent<ComfirmRessourceCreation>();
            butScr.setImageId(id);
        }
        else
        {
            confirmButton = GameObject.Find("ComfirmModifyButton");
            ConfirmModifyRessource butScr = confirmButton.GetComponent<ConfirmModifyRessource>();
            butScr.setImageId(id);

        }
    }

    public void SetRessourceImage(GameObject ressImage, int imgId)
    {
        ressourceImage = ressImage;
        id = imgId;
    }
}
