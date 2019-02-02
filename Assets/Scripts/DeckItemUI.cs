using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class DeckItemUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ButtonBehaviour m_EditButtonBehaviour;
    [SerializeField]
    private TextMeshProUGUI m_DeckName;
    [SerializeField]
    private TextMeshProUGUI m_MinCardLimit;
    [SerializeField]
    private TextMeshProUGUI m_MaxCardLimit;

    #region Properties
    public DeckListHandler DeckHandler { get; set; }
    public RectTransform RectTransform { get; private set; }

    public string DeckName
    {
        get
        {
            return m_DeckName.text;
        }
        set
        {
            m_DeckName.text = value;
        }
    }

    public int DeckCardLimitMin
    {
        get
        {
            return int.Parse(m_MinCardLimit.text);
        }
        set
        {
            m_MinCardLimit.text = value.ToString();
        }
    }

    public int DeckCardLimitMax
    {
        get
        {
            return int.Parse(m_MaxCardLimit.text);
        }
        set
        {
            m_MaxCardLimit.text = value.ToString();
        }
    }
    #endregion

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DeckHandler.deckSelected = DeckName;
    }

    public void Delete()
    {
        //todo
    }

    public void Edit()
    {
        //todo get info in bdd then pass param
       // m_EditButtonBehaviour.AddParam("DeckName", m_DeckName.text);
       // m_EditButtonBehaviour.AddParam("MinCardLimit", m_MinCardLimit.text);
       // m_EditButtonBehaviour.AddParam("MaxCardLimit", m_MaxCardLimit.text);
       //m_EditButtonBehaviour.SendToDispatch();
    }
}
