using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRange, spawnPerSecond;

    [SerializeField]
    private PigEnemy pigPrefab;

    [SerializeField]
    private List<Vector3SO> playerPositions;

    [SerializeField]
    private float spawnHeight;

    [SerializeField]
    private int maxActivePigs;


    public List<PigEnemy> activePigs, inactivePigs;




    private float spawnCounter;



    private void Update()
    {
        spawnCounter += Time.deltaTime;
        if (spawnCounter > 1.0f / spawnPerSecond)
        {
            spawnCounter = 0;

            if (activePigs.Count < maxActivePigs)
                SpawnPig();
        }
    }




    private void SpawnPig()
    {
        PigEnemy newPig;

        if (inactivePigs.Count > 0)
        {
            newPig = inactivePigs[0];
            inactivePigs.RemoveAt(0);
        }

        else
            newPig = Instantiate(pigPrefab);


        activePigs.Add(newPig);
        newPig.transform.position = ChooseSpawnPosition();
        newPig.transform.SetParent(transform);
    }



    private Vector3 ChooseSpawnPosition()
    {
        Vector3 newSpawnPosition;
        Vector2 pointInCircle = Vector2.zero;

        if (playerPositions.Count > 1)
        {
            //flip a coin
        }
        else
        {
            pointInCircle = new Vector2(playerPositions[0].position.x, playerPositions[0].position.z) + Random.insideUnitCircle * spawnRange;
        }


        return new Vector3(pointInCircle.x, spawnHeight, pointInCircle.y);
    }




    public void ReturnPigToPool(PigEnemy killedPig)
    {
        killedPig.gameObject.SetActive(false);
        activePigs.Remove(killedPig);
        inactivePigs.Add(killedPig);
    }








}
