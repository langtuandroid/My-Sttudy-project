using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

public class AeroplaneControllerBasedOnMesh : MonoBehaviour
{
    [Title("Navigation Mesh")]
    [SerializeField] private Mesh m_NavigationMesh;
    [FormerlySerializedAs("m_AeroplaneSpeed")]
    [Space(10)]  
    
    [Title("Aeroplane Data")]
    [SerializeField] private Transform m_AeroPlane;
    [SerializeField] private float m_AeroplaneSpeed;
    [SerializeField, Range(0.2f, 1f)] private float m_AeroPlaneRotateDuration;
    [Space(10)]

    private Vector3[] _navigationMeshVerts;
    private int _navigationMeshVertsIndex;

    private void Start()
    {
        _navigationMeshVertsIndex = 0;
        Navigate(); 
    }

    private void OnValidate() => _navigationMeshVerts = m_NavigationMesh.vertices;

    private void Navigate()
    {
        if(_navigationMeshVertsIndex >= _navigationMeshVerts.Length-1) return;

        var destination = _navigationMeshVerts[_navigationMeshVertsIndex];
        var distance = Vector3.Distance(m_AeroPlane.position, destination);
        var duration = distance / m_AeroplaneSpeed;

        m_AeroPlane.DOLookAt(destination, m_AeroPlaneRotateDuration).SetEase(Ease.Linear);
        
        m_AeroPlane.DOMove(destination, duration)
            .SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    _navigationMeshVertsIndex++;
                    Navigate();
                });
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        _navigationMeshVerts.ForEach(v => Gizmos.DrawSphere(v, 0.005f));
    }    
#endif
    
}
