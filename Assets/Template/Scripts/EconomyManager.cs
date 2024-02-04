using DG.Tweening;
using Game.Dev.Scripts;
using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class EconomyManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;

        private void OnEnable()
        {
            BusSystem.OnAddMoneys += AddMoneys;
            BusSystem.OnResetMoneys += ResetMoneys;
            BusSystem.OnSetMoneys += SetMoneyText;
        }

        private void OnDisable()
        {
            BusSystem.OnAddMoneys -= AddMoneys;
            BusSystem.OnResetMoneys -= ResetMoneys;
            BusSystem.OnSetMoneys -= SetMoneyText;
        }

        private void Start()
        {
            BusSystem.CallSetMoneys( SaveManager.instance.saveData.GetMoneys(), SaveManager.instance.saveData.GetMoneys());
        }

        #region AddMoney

        private void AddMoneys(int amount)
        {
            var oldAmount =  SaveManager.instance.saveData.GetMoneys();
            var newAmount = oldAmount + amount;

            BusSystem.CallSetMoneys(oldAmount,newAmount);
        }

        #endregion

        #region ResetMoney

        private void ResetMoneys()
        {
            var oldAmount = SaveManager.instance.saveData.GetMoneys();
            const int newAmount = 0;

            BusSystem.CallSetMoneys(oldAmount,newAmount);
        }

        #endregion

        #region SetMoney

        private void SetMoneyText(int oldAmount, int targetAmount)
        {
            SaveManager.instance.saveData.moneys = targetAmount;
            SaveManager.instance.Save();
            
            if (InitializeManager.instance.settingsData.useMoneyAnimation)
            {
                AnimateMoneyText(oldAmount, targetAmount);
            }
            else
            {
                moneyText.text = MoneyCalculator.NumberToStringFormatter(targetAmount);
            }
            
            // BusSystem.CallRefreshUpgradeValues();
        }
            
        private void AnimateMoneyText(int startAmount, int targetAmount)
        {
            DOTween.To(() => startAmount, x => startAmount = x, targetAmount, InitializeManager.instance.settingsData.moneyAnimationDuration)
                .OnUpdate(() => moneyText.text = MoneyCalculator.NumberToStringFormatter(startAmount))
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .SetSpeedBased(false);
        }

        #endregion
    }
}