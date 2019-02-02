using UnityEngine.EventSystems;
using UnityEngine;

public class CardItemUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ButtonBehaviour m_EditButtonBehaviour;
    
    #region Properties
    public DeckCardListHandler DeckHandler { get; set; }
    public RectTransform RectTransform { get; private set; }

    #endregion

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DeckHandler.cardSelected = this;
    }

    public void Edit()
    {
        //todo get param and info
       // m_EditButtonBehaviour.SendToDispatch();
    }

    public void Delete()
    {
        DeckHandler.DeleteCard(this);
    }
}
