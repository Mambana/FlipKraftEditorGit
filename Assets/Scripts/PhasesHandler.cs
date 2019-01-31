using System.Collections.Generic;
using UnityEngine;

public class PhasesHandler : MonoBehaviour
{
    public int phaseSelected;

    [SerializeField]
    private GameObject m_PphasesUIPrefab;
    [SerializeField]
    private List<PhaseItemUI> m_PhasesList;
    private Transform m_ScrollArea;
    private RectTransform m_Content;

    private void Start()
    {
        m_PhasesList = new List<PhaseItemUI>();
        m_ScrollArea = transform.GetChild(1);
        m_Content = m_ScrollArea.GetChild(0).GetComponent<RectTransform>();
        ReorganiseList();
    }

    public void AddPhase()
    {
        GameObject go = Instantiate(m_PphasesUIPrefab, m_Content);
        PhaseItemUI item = go.GetComponent<PhaseItemUI>();
        item.PhaseHandler = this;
        item.indexText.text = (m_PhasesList.Count - 1).ToString();
        m_PhasesList.Add(item);
        m_Content.sizeDelta = new Vector2(m_Content.sizeDelta.x, m_Content.sizeDelta.y + item.RectTransform.sizeDelta.y);
    }

    public void RemovePhase()
    {
        if (phaseSelected >= 0 && phaseSelected < m_PhasesList.Count)
        {
            PhaseItemUI item = m_PhasesList[phaseSelected];
            GameObject go = item.gameObject;
            m_Content.sizeDelta = new Vector2(m_Content.sizeDelta.x, m_Content.sizeDelta.y - item.RectTransform.sizeDelta.y);
            Destroy(go);
        }
        ReorganiseList();
    }

    public void ReorganiseList()
    {
        this.DelayAction(0.05f, () =>
        {
            int i = 0;
            m_PhasesList.Clear();
            foreach (Transform t in m_Content)
            {
                PhaseItemUI item = t.gameObject.GetComponent<PhaseItemUI>();
                item.PhaseHandler = this;
                item.indexText.text = i.ToString();
                m_PhasesList.Add(item);
                ++i;
            }
        });
    }
}
