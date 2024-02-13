using UnityEngine;

namespace Game.Dev.Scripts.ScriptableSO
{
    [CreateAssetMenu(fileName = "GameOptions", menuName = "ScriptableObjects/GameOptions")]
    public class GameOptions : ScriptableObject
    {
        [Header("Income")]
        public int winIncome;
        public int loseIncome;
    }
}