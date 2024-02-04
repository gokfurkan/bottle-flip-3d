using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class SettingsManager : MonoBehaviour
    {
        public GameObject soundOn;
        public GameObject soundOff;
        public GameObject hapticOn;
        public GameObject hapticOff;

        [Space(10)]
        public TextMeshProUGUI versionText;

        private void Start()
        {
            InitSettings();
            InitGameVersion();
        }

        public void ToggleSound()
        {
            bool currentSoundState = SaveManager.instance.saveData.GetSound();
    
            soundOn.SetActive(!currentSoundState);
            soundOff.SetActive(currentSoundState);
            
            AudioListener.volume = !currentSoundState ? 1 : 0;

            SaveManager.instance.saveData.sound = !currentSoundState;
            SaveManager.instance.Save();
        }

        public void ToggleHaptic()
        {
            bool currentHapticState = SaveManager.instance.saveData.GetHaptic();
    
            hapticOn.SetActive(!currentHapticState);
            hapticOff.SetActive(currentHapticState);
            
            MoreMountains.NiceVibrations.MMVibrationManager.SetHapticsActive(!currentHapticState);

            SaveManager.instance.saveData.haptic = !currentHapticState;
            SaveManager.instance.Save();
        }

        private void InitSettings()
        {
            bool currentSoundState = SaveManager.instance.saveData.GetSound();
            bool currentHapticState = SaveManager.instance.saveData.GetHaptic();

            SetActiveState(soundOn, soundOff, currentSoundState);
            SetActiveState(hapticOn, hapticOff, currentHapticState);

            void SetActiveState(GameObject onObject, GameObject offObject, bool state)
            {
                onObject.SetActive(state);
                offObject.SetActive(!state);
            }
        }

        private void InitGameVersion()
        {
            string title = "version ";
            versionText.text = title + Application.version;
        }
    }
}