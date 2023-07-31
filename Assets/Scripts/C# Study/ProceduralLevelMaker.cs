using System;
using System.Collections.Generic;
using UnityEngine;

namespace C__Study
{
    public class ProceduralLevelMaker : MonoBehaviour
    {
        [Serializable]
        public struct ProceduralObjectData
        {
            public GameObject m_Object;
            public int m_ObjectCount;
            public int m_PerObjectRadius;
            
            public Vector3 m_PlacementArea;
            public Color m_PlacementAreaColor;
        }

        public List<ProceduralObjectData> m_ProceduralObjectPlacement;

        private void OnValidate()
        {
            
        }

        void OnDrawGizmos()
        {
            foreach (ProceduralObjectData objectData in m_ProceduralObjectPlacement)
            {
                Gizmos.color = objectData.m_PlacementAreaColor;
                Gizmos.DrawWireCube(transform.position,objectData.m_PlacementArea);
            }
            
        }
    }
}