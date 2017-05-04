﻿using UnityEngine;
public abstract class LevelManagerBase : DerivableSingleton<LevelManagerBase>
{
    [SerializeField]
    private AudioClip SceneClip;

    protected virtual void Start()
    {
        SoundManager.PlaySoundtrack(SceneClip);
    }

    protected virtual void OnApplicationQuit()
    {

    }
}
