﻿using System;
using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class EnemyBugController : MonoBehaviour
    {

        [SerializeField] private Animator m_Animator;
        
        [SerializeField] private Vector3 m_PopUpPosition;
        [SerializeField] private float m_PopUpDuration;
        [SerializeField] private float m_PopUpDelay;
        
        private static readonly int Fly = Animator.StringToHash("Fly");
        private static readonly int Stuck = Animator.StringToHash("Stuck");
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_Animator.SetBool(Stuck, true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_Animator.SetBool(Stuck, false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X)) PopUp();
        }
        
        private void PopUp()
        {
            DOVirtual.DelayedCall(m_PopUpDelay, delegate
            {
                transform.DOMove(m_PopUpPosition, m_PopUpDuration).SetEase(Ease.Linear).OnComplete(delegate
                {
                    m_Animator.SetBool(Fly, true);
                });

            }, false);
        }
        
    }
}