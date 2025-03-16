using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    InfectionBehaviour infectionBehaviour;

    public Animator Ani;

    public Transform target;
    public float detectionRange = 10f;

    public float WalkSpeed;
    public float RunSpeed;
    public float CrawlSpeed;

    private NavMeshAgent agent;

    public bool HasAddedOneTooAmountOfZombies;
    public float InfectRange;

    public bool Cripel;

    public bool Death;

    public bool WaitAMoment;
    public float MomentTimer;

    void Start()
    {
        WalkSpeed = Random.Range(0.40f, 0.61f);
        RunSpeed = Random.Range(4.0f, 6.1f);

        Ani = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        infectionBehaviour = FindAnyObjectByType<InfectionBehaviour>();

        // Get the NavMeshAgent component attached to the character
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Death == true)
        {
            agent.SetDestination(transform.position);
            return;
        }

        if (WaitAMoment == true)
        {
            MomentTimer += Time.deltaTime;

            if (MomentTimer > 1)
            {
                Ani.SetBool("Attack", false);
                Ani.SetBool("Hit", false);
                MomentTimer = 0;
                WaitAMoment = false;
            }
            return;
        }

        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);

            if (Cripel == false)
            {
                if (Vector3.Distance(transform.position, target.position) <= detectionRange)
                {
                    agent.speed = RunSpeed;

                    Ani.SetBool("Run", true);
                    Ani.SetBool("Walk", false);

                    if (Vector3.Distance(transform.position, target.position) < .5f)
                    {
                        Ani.SetBool("Attack", true);
                        WaitAMoment = true;
                        infectionBehaviour.DamageInfection(5);
                    }
                }
                else
                {
                    agent.speed = WalkSpeed;

                    Ani.SetBool("Run", false);
                    Ani.SetBool("Walk", true);
                }
            }
            else
            {
                agent.speed = CrawlSpeed;

                Ani.SetBool("Run", false);
                Ani.SetBool("Walk", false);
                Ani.SetBool("Crawl", true);
            }

            if (Vector3.Distance(transform.position, target.position) <= InfectRange)
            {
                if (HasAddedOneTooAmountOfZombies == false)
                {
                    infectionBehaviour.amountOfZombiesNearby += 1;
                    HasAddedOneTooAmountOfZombies = true;
                }
            }
            else
            {
                if (HasAddedOneTooAmountOfZombies == true)
                {
                    infectionBehaviour.amountOfZombiesNearby -= 1;
                    HasAddedOneTooAmountOfZombies = false;
                }
            }
        }
    }
}

