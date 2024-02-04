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
    }

    public enum LevelTextType
    {
        Level,
        LevelCompleted,
        LevelFailed,
    }

    public enum CameraType
    {
        Menu,
        Game,
        End,
    }

    public enum AudioType
    {
        GameStart,
    }
}