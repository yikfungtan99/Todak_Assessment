﻿using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    public static T Instance { get; protected set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            throw new System.Exception($"{Instance.name} {gameObject.name} An instance of this singleton already exists.");
        }
        else
        {
            Instance = (T)this;
        }
    }
}