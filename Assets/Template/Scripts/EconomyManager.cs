using DG.Tweening;
using Game.Dev.Scripts;
using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class EconomyManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;

        private int oldMoneyTarget, newMoneyTarget;

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
            BusSystem.CallSetMoneys();
        }

        #region AddMoney

        private void AddMoneys(int amount)
        {
            var oldAmount =  SaveManager.instance.saveData.GetMoneys();
            var newAmount = oldAmount + amount;

            oldMoneyTarget = oldAmount;
            newMoneyTarget = newAmount;
            
            SaveManager.instance.saveData.moneys = newAmount;
            SaveManager.instance.Save();

            BusSystem.CallSetMoneys();
        }

        #endregion

        #region ResetMoney

        private void ResetMoneys()
        {
            SaveManager.instance.saveData.moneys = 0;
            SaveManager.instance.Save();

            BusSystem.CallSetMoneys();
        }

        #endregion

        #region SetMoney

        private void SetMoneyText()
        {
            if (oldMoneyTarget == 0 || newMoneyTarget == 0)
            {
                oldMoneyTarget = 0;
                newMoneyTarget = SaveManager.instance.saveData.GetMoneys();
            }
            
            if (InitializeManager.instance.settingsData.useMoneyAnimation)
            {
                AnimateMoneyText(oldMoneyTarget, newMoneyTarget);
            }
            else
            {
                moneyText.text = MoneyCalculator.NumberToStringFormatter(newMoneyTarget);
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