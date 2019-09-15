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

    private UIManager uiManager;

    public AudioSource hitSOund;


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


        activePlayers = new List<Transform>();


        currentLifes = maxLifes;
        pigsKilled = 0;

        uiManager = FindObjectOfType<UIManager>();
    }




    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    [ContextMenu("HitPig")]
    public void OnHitPig()
    {
        hitSOund.Play();
        currentLifes--;
        if (currentLifes == 0)
        {
            activePlayers[0].gameObject.GetComponent<AudioListener>().enabled = false;
            uiManager.ShowGameOverPanel();

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("new game");
            NewGame();
        }
    }

}
