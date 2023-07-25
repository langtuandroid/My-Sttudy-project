using UnityEngine;

namespace C__Study
{
    public class Program : MonoBehaviour
    {
        private void Start()
        {
             EnemyClass<EnemyMinion> enemyMinion = new EnemyClass<EnemyMinion>();
             enemyMinion.GetDamage(new EnemyMinion());
        }
    }

    public class EnemyClass<T> where T : IEnemy
    {
        public void GetDamage(T value)
        {
            value.Damage();
        }
    }

    public interface IEnemy
    {
        public void Damage();
    }

    public class EnemyMinion : IEnemy
    {
        public void Damage()
        {
            Debug.Log("Enemy Minion Damage!!");
        }
    }

}