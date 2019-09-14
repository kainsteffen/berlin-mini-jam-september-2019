using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PigEnemy : MonoBehaviour
{
    enum State
    {
        None, Falling, Moving,
    }

    public List<Transform> activePlayers;
    public GameObject deathExplosion;
    public float animSpeed;
    public float speedBoost;
    public Vector3 growSize;
    public Vector3 shrinkSize;

    private Vector3 startSize;
    private float startSpeed;
    private State state;
    private NavMeshAgent nav;
    private Rigidbody rb;
    private float moveAnimValue;
    private bool scalingUp = false;

    private bool onGround;


    [SerializeField]
    private float destinationChecktime;

    private float destinationCheckCounter;


    private PigSpawner spawner;
    private DistanceToPlayersChecker distanceChecker;


    private void Awake()
    {
        distanceChecker = GetComponent<DistanceToPlayersChecker>();
        activePlayers = GameManager.Instance.activePlayers;
    }


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        startSize = transform.localScale;
        startSpeed = nav.speed;
        SetState(State.Falling);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Falling:
                if (rb.velocity.magnitude == 0)
                {
                    SetState(State.Moving);
                }
                break;
            case State.Moving:
                PlayMoveAnimation();
                break;
        }


        if (onGround)
            destinationCheckCounter -= Time.deltaTime;
        if (destinationCheckCounter <= 0)
        {
            nav.SetDestination(GetClosestPayer());
            destinationCheckCounter = destinationChecktime;

        }
    }

    public void Activate(PigSpawner spawnedFrom)
    {

        spawner = spawnedFrom;
        onGround = false;
        destinationCheckCounter = destinationChecktime;


    }

    private void PlayMoveAnimation()
    {
        if (scalingUp)
        {
            moveAnimValue = Mathf.Lerp(moveAnimValue, 1, animSpeed);
        }
        else
        {
            moveAnimValue = Mathf.Lerp(moveAnimValue, 0, animSpeed);
        }

        if (moveAnimValue > 0.99)
        {
            scalingUp = false;
        }
        else if (moveAnimValue < 0.01)
        {
            scalingUp = true;
        }

        nav.speed = startSpeed + moveAnimValue * speedBoost;

        transform.localScale = Vector3.Lerp(growSize, shrinkSize, moveAnimValue);

    }

    private void SetState(State newState)
    {
        switch (this.state)
        {
            case State.Falling:
                switch (newState)
                {
                    case State.Moving:
                        nav.enabled = true;
                        if (nav.enabled)
                        {
                            //  nav.SetDestination(GetClosestPayer());
                        }
                        break;
                }
                break;
            case State.None:
                switch (newState)
                {
                    case State.Falling:
                        nav.enabled = false;
                        break;
                }
                break;
        }
        state = newState;
    }

    private Vector3 GetClosestPayer()
    {
        if (activePlayers.Count == 1)
        {
            return activePlayers[0].position;
        }
        else
        {
            Transform targetedPlayer = activePlayers[Random.Range(0, activePlayers.Count)];
            return targetedPlayer.position;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Ground")
        {
            onGround = true;
            SetState(State.Moving);

        }
        else if(collision.collider.gameObject.tag == "Player")
        {
            GameManager.Instance.OnHitPig();
        }
    }



    public void Kill()
    {
        Instantiate(deathExplosion, transform.position, deathExplosion.transform.rotation);
        spawner.ReturnPigToPool(this);
    }

}
