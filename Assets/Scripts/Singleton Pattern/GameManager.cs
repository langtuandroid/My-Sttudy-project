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
            _sessionStartTime = DateTime.Now;
            Debug.Log("Session Start @: " + _sessionStartTime);
            
            //TODO
            // - Load Player Save
            // - If not Save.....
            // - daly rewards....
        }

        private void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;
            Debug.Log("Session Start @: " + _sessionEndTime);

            TimeSpan span = _sessionEndTime.Subtract(_sessionStartTime);
            Debug.Log("Session Span @: " + span);
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Next Scene"))
            {
                if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) 
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (GUILayout.Button("last Scene"))
            { 
                if(SceneManager.GetActiveScene().buildIndex > 0)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }
}