using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIdleSMB : SceneLinkedSMB<ButtonBehaviour> {

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.ChangeImgSpriteToIdle();
    }
}
