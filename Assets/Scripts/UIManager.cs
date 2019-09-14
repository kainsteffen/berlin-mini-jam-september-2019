using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI scoreText, healthText;


    private void Update()
    {
        scoreText.SetText("Score " + GameManager.Instance.pigsKilled.ToString());
        healthText.SetText("Health " + GameManager.Instance.currentLifes.ToString());
    }



}
