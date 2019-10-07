using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeckListHandler : MonoBehaviour
{
    public string deckSelected;

    [SerializeField]
    private GameObject m_DeckUIPrefab;
    [SerializeField]
    private List<DeckItemUI> m_DecksList;
    private Transform m_ScrollArea;
    private RectTransform m_Content;

    private void Start()
    {
        m_DecksList = new List<DeckItemUI>();
        m_ScrollArea = transform.GetChild(1);
        m_Content = m_ScrollArea.GetChild(0).GetComponent<RectTransform>();
        foreach (Transform t in m_Content)
        {
            DeckItemUI item = t.gameObject.GetComponent<DeckItemUI>();
            item.DeckHandler = this;
            m_DecksList.Add(item);
        }
    }

    public void AddDeck(string projectId, string projectName, string id, string cardName)
    {
        GameObject go = Instantiate(m_DeckUIPrefab, m_Content);
        DeckItemUI item = go.GetComponent<DeckItemUI>();
        item.DeckHandler = this;
        m_DecksList.Add(item);
        m_Content.sizeDelta = new Vector2(m_Content.sizeDelta.x + item.RectTransform.sizeDelta.x, m_Content.sizeDelta.y);
       // go.GetComponentInChildren<TMP_InputField>().textViewport = m_Content;
        go.GetComponentInChildren<TextMeshProUGUI>().text = cardName;
        go.GetComponentInChildren<ModifyCardButton>().setIdToModify(id);
        go.GetComponentInChildren<ModifyCardButton>().setProjectId(projectId);
        go.GetComponentInChildren<ModifyCardButton>().setProjectName(projectName);


    }

    public void RemoveAllDeck()
    {
        GameObject go = null;
        foreach (DeckItemUI item in m_DecksList)
        {
            m_Content.sizeDelta = new Vector2(m_Content.sizeDelta.x - item.RectTransform.sizeDelta.x, m_Content.sizeDelta.y);
            go = item.gameObject;
            Destroy(go);
        }
        m_DecksList.Clear();
    }
    public void RemoveDeck()
    {
        DeckItemUI item = m_DecksList.Find(x => x.DeckName == deckSelected);
        GameObject go = null;
        if (item)
        {
            go = item.gameObject;
        }
        else if (m_DecksList.Count > 0)
        {
            item = m_DecksList[m_DecksList.Count - 1];
            go = item.gameObject;
        }
        if (go)
        {
            m_Content.sizeDelta = new Vector2(m_Content.sizeDelta.x - item.RectTransform.sizeDelta.x, m_Content.sizeDelta.y);
            m_DecksList.Remove(item);
            Destroy(go);
        }
    }
}
