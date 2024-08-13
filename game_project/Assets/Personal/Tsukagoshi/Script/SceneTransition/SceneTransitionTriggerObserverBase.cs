using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�V�[���̏�Ԃ��Ď����A�����𖞂����ƑJ�ڂ��g���K�[����r�w�C�r�A�̊��</summary>
// �u�^�C�g�����C���Q�[���v�u�C���Q�[�������U���g�v�u���U���g���^�C�g���v�ւ̑J�ڂ֔h�����邱�Ƃ�z��
public abstract class SceneTransitionTriggerObserverBase : MonoBehaviour
{
    [SerializeField]
    protected string nextSceneName;

    // Update is called once per frame
    protected void Update()
    {
        // �J�ڏ����𖞂������瑦���J�ڂ���
        if (ShouldTrigger())
        {
            SceneLoader.Load(nextSceneName);
        }
    }

    /// <summary>�V�[���J�ڂ��g���K�[���ׂ����� True ��Ԃ�</summary>
    protected abstract bool ShouldTrigger();
}
