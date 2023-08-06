using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Singleton_Pattern
{
    public class GameManager : Singleton<GameManager>
    {
        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;

        private void Start()
        {
            //TODO
            // - Load player save
            // - If no save, redirect player to registration scene
            // - Call backend and get daily challenge and rewards

            _sessionStartTime = DateTime.Now;

            Debug.Log("Game Session Start @ : " + DateTime.Now);
        }

        private void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;
            
            Debug.Log("Game Session End @ : " + DateTime.Now);

            TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);
            Debug.Log("Game session lasted: " + timeDifference);

        }

        private void OnGUI()
        {
            if(GUILayout.Button("Next Scene"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}