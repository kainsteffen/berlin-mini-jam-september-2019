using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance { get; private set; }




    public int maxLifes, currentLifes;

    public event Action GameOverEvent = delegate { };


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void NewGame()
    {
        currentLifes = maxLifes;
    }


    [ContextMenu("HitPig")]
    public void OnHitPig()
    {
        currentLifes--;
        if (currentLifes == 0)
            GameOverEvent();
    }



}
