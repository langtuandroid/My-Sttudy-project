using UnityEngine;

namespace C__Study
{
    public class Program : MonoBehaviour
    {
        public delegate void TestDelegate();

        private TestDelegate _testDelegateFunction;

        private void Start()
        {
            _testDelegateFunction = delegate { Debug.Log("Anzamul Haque Akash 1"); };
            _testDelegateFunction += delegate { Debug.Log("Anzamul Haque Akash 2"); };

            _testDelegateFunction();
        }

    }
    
}