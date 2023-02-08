using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    public Transform closestPlayer;
    public List<Transform> playerPos = new List<Transform>();

    public Transform patrolRoute;
    public List<Transform> locations;
    public int damage = 1;
    public Slider monsterSlider;


    private int locationIndex = 0;
    private NavMeshAgent agent;
    private int _lives = 5;

    public Animator animator;


    public int EnemyLives
    {
        get { return _lives; }
        private set
        {
            _lives = value;
            if (_lives <= 0)
            {
                closestPlayer.GetComponent<CharacterMovement>().MonsterAttackBoarder.SetActive(false);
                foreach (CharacterMovement cm in FindObjectsOfType<CharacterMovement>())
                {
                    cm.inRangeMonster = false;
                }
                Destroy(transform.root.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //player = GameObject.Find("Player").transform;
        foreach (GameObject go in PlayerSpawning.instance.players)
        {
            if (go != null)
            {
                playerPos.Add(go.transform);
            }
        }

        monsterSlider.maxValue = EnemyLives;
        monsterSlider.value = EnemyLives;
       
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void Update()
    {
        //foreach (Transform t in playerPos)
        //{
        //    float dist = (t.position - transform.position).magnitude;
        //}
        closestPlayer = playerPos[0];
        //closestPlayer = playerPos.AsQueryable().Min();
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }


        
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;

        //anim
        if (animator.gameObject.name.Contains("Glise")) //aka Cyclopes
        {
            animator.SetBool("isWalkGlise", true);
        }
        else if (animator.gameObject.name.Contains("Jelly")) // aka Glise
        {
            animator.SetBool("isWalkJelly", true);
        }
        else if (animator.gameObject.name.Contains("AttackGlise")) // Still sand
        {
            animator.SetBool("isWalkSand", true);
        }

        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    public void TakeDamage()
    {
        monsterSlider.value -= damage;
        EnemyLives -= damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            agent.destination = closestPlayer.position;

            if (animator.gameObject.name.Contains("Glise")) //aka Cyclopes
            {
                animator.SetBool("isWalkGlise", false);
                animator.Play("AttackCycl", 0, 0f);
            }
            else if (animator.gameObject.name.Contains("Jelly")) // aka Jelly
            {
                animator.SetBool("isWalkJelly", false);
                animator.Play("AttackJelly", 0, 0f);
            }
            else if (animator.gameObject.name.Contains("AttackGlise")) // Still sand
            {
                animator.SetBool("isWalkSand", false);
                animator.Play("AttackSand", 0, 0f);
            }
            Debug.Log("Player detected - attack!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            if (animator.gameObject.name.Contains("Glise")) //aka Cyclopes
            {
                animator.SetBool("isWalkCycl", true);
            }
            else if (animator.gameObject.name.Contains("Jelly")) // aka Glise
            {
                animator.SetBool("isWalkJelly", true);
            }
            else if (animator.gameObject.name.Contains("AttackGlise")) // Still sand
            {
                animator.SetBool("isWalkSand", true);
            }
            Debug.Log("Player out of range, resume patrol");
        }
    }


   
}
