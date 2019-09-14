using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRange, spawnPerSecond;

    [SerializeField]
    private PigEnemy pigPrefab;


    private List<Transform> activePlayers;


    [SerializeField]
    private float spawnHeight;

    [SerializeField]
    private int maxActivePigs;


    public List<PigEnemy> activePigs, inactivePigs;




    private float spawnCounter;



    private void Awake()
    {
        activePlayers = GameManager.Instance.activePlayers;
    }



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
            print("spawnFroiPool");
            newPig = inactivePigs[0];
            inactivePigs.RemoveAt(0);
        }
        else
        {
            newPig = Instantiate(pigPrefab);
        }


        activePigs.Add(newPig);
        newPig.transform.position = ChooseSpawnPosition();
        newPig.transform.SetParent(transform);
        newPig.Activate(this);
        newPig.gameObject.SetActive(true);
    }



    private Vector3 ChooseSpawnPosition()
    {
        Vector3 newSpawnPosition;
        Vector2 pointInCircle = Vector2.zero;

        if (activePlayers.Count > 1)
        {
            //flip a coin
        }
        else
        {
            pointInCircle = new Vector2(activePlayers[0].position.x, activePlayers[0].position.z) + Random.insideUnitCircle * spawnRange;
        }


        return new Vector3(pointInCircle.x, spawnHeight, pointInCircle.y);
    }




    public void ReturnPigToPool(PigEnemy killedPig)
    {
        print("returning");
        killedPig.gameObject.SetActive(false);
        activePigs.Remove(killedPig);
        inactivePigs.Add(killedPig);
        print(inactivePigs.Count);
    }








}
