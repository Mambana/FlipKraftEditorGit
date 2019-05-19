using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHandler : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spriteListes;
    private Dictionary<int, Sprite> ressSpriteAssocs;
    // Start is called before the first frame update
    void Start()
    {
        ressSpriteAssocs = new Dictionary<int, Sprite>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRessSpriteAsoc(int ressId, Sprite spr)
    {
        ressSpriteAssocs.Add(ressId, spr);
    }

    public Sprite[] GetSprites()
    {
        return (spriteListes);
    }

    public Sprite GetSprite(int id)
    {
        return (spriteListes[id]);
    }
}
