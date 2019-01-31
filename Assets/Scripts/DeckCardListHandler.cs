using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCardListHandler : MonoBehaviour
{
    public CardItemUI cardSelected;

    [SerializeField]
    private GameObject m_CardUIPrefab;
    [SerializeField]
    private List<CardItemUI> m_CardsList;
    private Transform m_ScrollArea;
    private RectTransform m_Content;

    private void Start()
    {
        m_CardsList = new List<CardItemUI>();
        m_ScrollArea = transform.GetChild(1);
        m_Content = m_ScrollArea.GetChild(0).GetComponent<RectTransform>();
        foreach (Transform t in m_Content)
        {
            CardItemUI item = t.gameObject.GetComponent<CardItemUI>();
            item.DeckHandler = this;
            m_CardsList.Add(item);
        }
    }

    public void AddCard()
    {
        GameObject go = Instantiate(m_CardUIPrefab, m_Content);
        CardItemUI item = go.GetComponent<CardItemUI>();
        item.DeckHandler = this;
        m_CardsList.Add(item);
        if (m_CardsList.Count != 0 && m_CardsList.Count % 4 == 0)
            m_Content.sizeDelta = new Vector2(m_Content.sizeDelta.x, m_Content.sizeDelta.y + 325);
    }

    public void DeleteCard(CardItemUI cardItem)
    {
        CardItemUI item = cardItem && m_CardsList.Contains(cardItem) ? cardItem : m_CardsList.Find(x => x == cardSelected);
        GameObject go = null;
        if (item)
        {
            go = item.gameObject;
        }
        else if (m_CardsList.Count > 0)
        {
            item = m_CardsList[m_CardsList.Count - 1];
            go = item.gameObject;
        }
        if (go)
        {
            if (m_CardsList.Count != 0 && m_CardsList.Count % 4 == 0)
                m_Content.sizeDelta = new Vector2(m_Content.sizeDelta.x, m_Content.sizeDelta.y - 325);
            m_CardsList.Remove(item);
            Destroy(go);
        }
    }
}
