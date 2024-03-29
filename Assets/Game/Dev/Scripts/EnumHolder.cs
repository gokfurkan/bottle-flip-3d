﻿namespace Game.Dev.Scripts
{
    public enum SceneType
    {
        Load,
        Game,
    }

    public enum PanelType
    {
        Dev,
        OpenSettings,
        Settings, 
        Win, 
        Lose, 
        Level, 
        Money, 
        Restart,
        LevelProgress,
        Shop,
        OpenShop,
        EndContinue,
    }

    public enum LevelTextType
    {
        Level,
        LevelCompleted,
        LevelFailed,
    }

    public enum EndIncomeType
    {
        Win,
        Lose,
    }

    public enum CameraType
    {
        Menu,
        Game,
        End,
    }

    public enum AudioType
    {
        Win,
        Lose,
        Bottle,
    }
    
    public enum SkinRarity
    {
        Standard,
        Vip,
        Epic
    }
}