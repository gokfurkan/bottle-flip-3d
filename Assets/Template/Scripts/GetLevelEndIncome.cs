using System;
using Game.Dev.Scripts;
using Game.Dev.Scripts.ScriptableSO;
using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class GetLevelEndIncome : MonoBehaviour
    {
        public GameOptions gameOptions;
        public EndIncomeType incomeType;
        public TextMeshProUGUI incomeText;

        private void OnEnable()
        {
            var incomeAmount = 0;
            
            switch (incomeType)
            {
                case EndIncomeType.Win:
                    incomeAmount = gameOptions.winIncome;
                    break;
                case EndIncomeType.Lose:
                    incomeAmount = gameOptions.loseIncome;
                    break;
            }

            incomeText.text = incomeAmount.ToString();
        }
    }
}