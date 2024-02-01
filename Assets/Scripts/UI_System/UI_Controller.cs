using System;
using System.Collections.Generic;
using UI_System.UI_Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI_System
{
    public class UI_Controller : MonoBehaviour
    {
        private List<GameObject> _uiActivies = new List<GameObject>();

        public event Action<ScreenType> OnScreenSwitch;
    
        void Start()
        {
            var activeScreens = new List<GameObject>();
            foreach (var screen in UI_ScreenRepository.Screens)
                if (screen.isActiveAndEnabled) activeScreens.Add(screen.gameObject);

            _uiActivies = activeScreens;
            if (_uiActivies.Count <= 0) Debug.LogWarning("No have active screen");
        }
        
        public void SetScreen(ScreenType screen)
        {
            for (int i = 0; i < _uiActivies.Count; i++)
                _uiActivies[i].SetActive(false);

            _uiActivies = new List<GameObject>();
        
            UI_ScreenBase newScreen = UI_ScreenRepository.GetScreen(screen);
            newScreen.gameObject.SetActive(true);
            _uiActivies.Add(newScreen.gameObject);
            OnScreenSwitch?.Invoke(screen);
        }    
        
        public void LoadScene(int sceneBuildIndex)
        {
            if (sceneBuildIndex == -1)
            {
                int currentSceneNum = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneNum, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(sceneBuildIndex);
            }
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}