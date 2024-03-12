using System;
using System.Collections.Generic;
using System.Linq;
using UI_System.UI_Screens;
using UnityEngine;
using AudioSettings = UI_System.UI_Screens.AudioSettings;

namespace UI_System
{
    public class UI_ScreenRepository : MonoBehaviour
    {
        private Dictionary<Type, UI_ScreenBase> _screens;

        private static UI_ScreenRepository _instance;
        public static IReadOnlyList<UI_ScreenBase> Screens => _instance._screens.Values.ToArray();

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;

            _screens = new Dictionary<Type, UI_ScreenBase>();

            UI_ScreenBase[] uiScreens = FindObjectsOfType<UI_ScreenBase>(true);
            foreach (UI_ScreenBase screen in uiScreens) _screens.Add(screen.GetType(), screen);
        }

        public static TScreen GetScreen<TScreen>() where TScreen : UI_ScreenBase
        {
            if (_instance == null) throw new NullReferenceException($"Instance is null");

            if (!_instance._screens.TryGetValue(typeof(TScreen), out UI_ScreenBase screen))
            {
                Debug.LogWarning("Error: invalid parameter in SetWindow(ScreenEnum screen)");
                return default;
            }

            return (TScreen)screen;
        }
    
        public static UI_ScreenBase GetScreen(ScreenType screenType)
        {
            if (_instance == null) throw new NullReferenceException($"Instance is null");
        
            switch (screenType)
            {     
                case ScreenType.MainMenu:
                    return GetScreen<MainMenuScreen>();
                case ScreenType.Rules:
                    return GetScreen<RulesScreen>();
                case ScreenType.GameplayWin:
                    return GetScreen<GameplayWinScreen>();
                case ScreenType.GameplayMain:
                    return GetScreen<GameplayMainScreen>();
                case ScreenType.GameplayLoose:
                    return GetScreen<GameplayLooseScreen>();
                case ScreenType.AudioSettings:
                    return GetScreen<AudioSettings>();
                case ScreenType.LocationWin:
                    return GetScreen<LocationWin>();
                default:
                    Debug.LogWarning("Error: invalid parameter in GetScreenByEnum(ScreenEnum screen)");
                    return default;
            }
        }
    }
}