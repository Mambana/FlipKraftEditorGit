using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHighlightInSMB : SceneLinkedSMB<ButtonBehaviour> {

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.ChangeImgSpriteToHighlighted();
    }
}
