using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTEventListener : MonoBehaviour
{


    private void Start()
    {
        GameManager.Instance.GameOverEvent += ImDead;
    }
    private void OnDisable()
    {
        GameManager.Instance.GameOverEvent -= ImDead;
    }


    private void ImDead()
    {
        print("dead");
    }

}