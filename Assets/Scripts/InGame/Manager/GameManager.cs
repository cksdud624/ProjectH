using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singleton = new GameObject("Game Manager");
                instance = singleton.AddComponent<GameManager>();
                DontDestroyOnLoad(singleton);
            }
            return instance;
        }
    }
}
