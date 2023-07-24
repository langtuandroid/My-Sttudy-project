using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace C__Study
{
    public class Program : MonoBehaviour
    {
        [System.Serializable]
        public struct Student
        {
            public int m_ID;
            public string m_Name;
        }

        public List<Student> m_Students;
        
        private void Start()
        {
            var sortedById = m_Students.OrderBy(s => s.m_ID);

            foreach (var student in sortedById)
            {
                Debug.Log("Name : " + student.m_Name + "\n" + "ID : " + student.m_ID + "\n");
            }
        }
    }
    
}