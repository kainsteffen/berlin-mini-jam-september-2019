using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToPlayersChecker : MonoBehaviour
{
 
 

    [SerializeField]
    private float tooFarDistance;

    private PigEnemy pigRoot;


    private void Awake()
    {
        pigRoot = GetComponent<PigEnemy>();
    }


    private void Start()
    {
        InvokeRepeating("CheckIfCloseToPlayers", 3f, 3f);
    }


    private void CheckIfCloseToPlayers()
    {
        bool tooFarFromPlayers = true;

        foreach (var position in pigRoot.activePlayers)
        {
            if(Vector3.Distance(transform.position, position.position) < tooFarDistance)
            {
                tooFarFromPlayers = false;
            }
        }

        if(tooFarFromPlayers)
        {
            pigRoot.Kill();
        }

    }


}
