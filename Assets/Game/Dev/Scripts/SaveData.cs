using System;
using System.Collections.Generic;

namespace Game.Dev.Scripts
{
    [Serializable]
    public class SaveData
    {
        public int level;
        public int moneys;

        public bool sound;
        public bool haptic;

        public int currentSkin;
        public int firstSetUnlockStatus = 0;
        public List<bool> skinsUnlockStatus = new List<bool>(27);
        

        public int GetLevel()
        {
            return level;
        }

        public int GetMoneys()
        {
            return moneys;
        }

        public bool GetSound()
        {
            return sound;
        }

        public bool GetHaptic()
        {
            return haptic;
        }
    }
}