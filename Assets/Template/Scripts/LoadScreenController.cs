using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Template.Scripts
{
    public class LoadScreenController : MonoBehaviour
    {
        [SerializeField] private SettingsData settingsData;
        [SerializeField] private Image loadingBar;
        
        private int fillOptionIndex;
        private float fillAmount = 0f;

        private void Start()
        {
            StartCoroutine(RunLoadingStages());
        }

        private IEnumerator RunLoadingStages()
        {
            for (int i = 0; i < settingsData.loadFillOption.Count; i++)
            {
                yield return FillBarToValue(settingsData.loadFillOption[i].stageAmount, settingsData.loadFillOption[i].stageDuration);
                yield return new WaitForSeconds(settingsData.loadFillOption[i].nextStageDelay);
            }

            SceneManager.LoadScene((int)settingsData.nextSceneAfterLoad);
        }

        private IEnumerator FillBarToValue(float targetValue, float duration)
        {
            float elapsedTime = 0f;
            float startValue = fillAmount;

            while (elapsedTime < duration)
            {
                fillAmount = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
                loadingBar.fillAmount = fillAmount;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            fillAmount = targetValue;
            loadingBar.fillAmount = fillAmount;
        }
    }

    [Serializable]
    public class LoadFillOptions
    {
        public float stageAmount;
        public float stageDuration;
        public float nextStageDelay;
    }
}