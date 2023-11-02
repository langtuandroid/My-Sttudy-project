using UnityEngine;

namespace FK_3.Player.Ammo
{
    [CreateAssetMenu(fileName = "Player", menuName = "Player/ProjectileTypeSo")]
    public class ProjectileTypeSo : ScriptableObject
    {
        public GameObject m_Projectile;
    }
}