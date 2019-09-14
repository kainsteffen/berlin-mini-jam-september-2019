using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance { get; private set; }

    public int pigsKilled;


    public int maxLifes, currentLifes;

    public event Action GameOverEvent = delegate { };


    public List<Transform> activePlayers;




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
        pigsKilled = 0;
        activePlayers = new List<Transform>();

        SceneManager.LoadScene("TestGameSceneJulian");
    }


    [ContextMenu("HitPig")]
    public void OnHitPig()
    {
        currentLifes--;
        if (currentLifes == 0)
            GameOverEvent();
    }



}
