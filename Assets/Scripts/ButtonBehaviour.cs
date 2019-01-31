using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private Sprite m_HighlightedButton;
    [SerializeField]
    private Sprite m_IdleButton;
    [SerializeField]
    private string m_UiNameToCall;
    [SerializeField]
    private Dictionary<string, string> m_Param = null;
    private Animator m_Animator;
    private Image m_Img;

    private void Start()
    {
      
        m_Img = GetComponent<Image>();
        m_Animator = GetComponent<Animator>();
        if (m_Animator)
            SceneLinkedSMB<ButtonBehaviour>.Initialise(m_Animator, this);
    }

    public void ChangeImgSpriteToHighlighted()
    {
        m_Img.sprite = m_HighlightedButton;
    }

    public void ChangeImgSpriteToIdle()
    {
        m_Img.sprite = m_IdleButton;
    }

    public void AddParam(string key, string ToAdd)
    {
        if (m_Param == null)
            m_Param = new Dictionary<string, string>();
        if (!m_Param.ContainsKey(ToAdd))
            m_Param.Add(key, ToAdd);
        else
            m_Param[key] = ToAdd;
    }

    public void SetParams(Dictionary<string, string> l)
    {
        m_Param = l;
    }

    public Dictionary<string, string> GetParam()
    {
        return m_Param;
    }

    public void SendToDispatch()
    {
        if (m_UiNameToCall == "")
        {
            Debug.LogWarning("The UIName is not defined");
            return;
        }
        Dispatcher.Instance.dispatch(m_UiNameToCall,"", m_Param);
    }

    public bool HasKey(string key)
    {
        return m_Param.ContainsKey(key);
    }
}
