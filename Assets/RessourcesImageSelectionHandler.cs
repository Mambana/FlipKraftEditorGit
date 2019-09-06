using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourcesImageSelectionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject m_imgButton;
    [SerializeField]
    private GameObject ressImage;
    private ImageHandler imagesHandler;
   

    void Start()
    {
        imagesHandler = GameObject.Find("ImageHandler").GetComponent<ImageHandler>();
        Sprite[] sprListe = imagesHandler.GetSprites();
        int i = 0;
        foreach (Sprite spr in sprListe)
        {
            GameObject imgButton = Instantiate(m_imgButton) as GameObject;

            imgButton.GetComponent<ImageSelectionButton>().SetRessourceImage(ressImage, i);
            imgButton.GetComponent<Image>().sprite = spr;
            imgButton.transform.SetParent(gameObject.transform, false);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
