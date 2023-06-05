using UnityEngine;

namespace Game_Development_Patterns_with_Unity_2021.Game_Manager_with__Singleton_
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance != null) return _instance;
                    GameObject obj = new GameObject
                    {
                        name = nameof(T)
                    };
                    _instance = obj.AddComponent<T>();
                }
                return _instance;
            }
        }

        public virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}