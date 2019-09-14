using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI scoreText, healthText;

    [SerializeField]
    private GameObject gameOverPanel;


    private void Update()
    {
        scoreText.SetText("Score " + GameManager.Instance.pigsKilled.ToString());
        healthText.SetText("Health " + GameManager.Instance.currentLifes.ToString());
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
