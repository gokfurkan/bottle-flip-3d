using Sirenix.OdinInspector;
using Template.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Dev.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        [ReadOnly] public bool isGameStart;
        [ReadOnly] public bool isLevelPlaying;
        [ReadOnly] public bool isLevelEnd;
        [ReadOnly] public bool isLevelWin;
        [ReadOnly] public bool isLevelLose;

        protected override void Initialize()
        {
            base.Initialize();

            OnGameStart();
        }
        
        private void OnEnable()
        {
            BusSystem.OnLevelStart += OnLevelStart;
            BusSystem.OnLevelEnd += OnLevelEnd;

            BusSystem.OnLevelLoad += OnLevelLoad;
        }

        private void OnDisable()
        {
            BusSystem.OnLevelStart -= OnLevelStart;
            BusSystem.OnLevelEnd -= OnLevelEnd;
            
            BusSystem.OnLevelLoad -= OnLevelLoad;
        }

        private void Update()
        {
            InputControl();
        }
        
        private void OnGameStart()
        {
            isGameStart = true;
        }

        private void OnLevelStart()
        {
            isLevelPlaying = true;
        }

        private void OnLevelEnd(bool win)
        {
            if (isLevelEnd) return;
            
            isLevelPlaying = false;
            isLevelEnd = true;
            
            if (win)
            {
                SaveManager.instance.saveData.level++;
                SaveManager.instance.Save();
                isLevelWin = true;
            }
            else
            {
                isLevelLose = true;
            }
        }

        private void OnLevelLoad()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

        private void InputControl()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isLevelPlaying)
                {
                    OnLevelStart();
                    BusSystem.CallLevelStart();
                }

                BusSystem.CallMouseClickDown();
            }

            if (Input.GetMouseButton(0))
            {
                BusSystem.CallMouseClicking();
            }

            if (Input.GetMouseButtonUp(0))
            {
                BusSystem.CallMouseClickUp();
            }
        }
    }
}