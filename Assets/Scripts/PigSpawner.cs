using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRange, spawnPerSecond;

    [SerializeField]
    private MockPig pigPrefab;

    [SerializeField]
    private List<Vector3SO> playerPositions;

    [SerializeField]
    private float spawnHeight;

    [SerializeField]
    private int maxActivePigs;


    public List<MockPig> activePigs, inactivePigs;




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
        MockPig newPig;

        if (inactivePigs.Count > 0)
            newPig = inactivePigs[0];

        else
            newPig = Instantiate(pigPrefab);

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
            pointInCircle = new Vector2(playerPositions[0].playerPosition.x, playerPositions[0].playerPosition.z) + Random.insideUnitCircle * spawnRange;
        }


        return new Vector3(pointInCircle.x, spawnHeight, pointInCircle.y);



    }









}
