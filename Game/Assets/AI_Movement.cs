using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    public Transform target;
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 60f;
    Vector3 WounderingPos;

    public float WalkSpeed;
    public float RunSpeed;

    private NavMeshAgent agent;
    private bool isChasing = false;

    public bool HasAddedOneTooAmountOfZombies;
    public float InfectRange;


    void Start()
    {
        // Get the NavMeshAgent component attached to the character
        agent = GetComponent<NavMeshAgent>();

        GetNewDirection();
    }

    void Update()
    {
        if (agent != null && target != null)
        {
            if (CanSeeTarget() == true)
            {
                isChasing = true;
            }
            else if (Vector3.Distance(transform.position, target.position) > detectionRange)
            {
                isChasing = false;
            }

            if (isChasing == true)
            {
                agent.SetDestination(target.position);

                agent.speed = RunSpeed;
            }
            else
            {
                agent.speed = WalkSpeed;

                if (Vector3.Distance(transform.position, WounderingPos) < .1)
                {
                    GetNewDirection();
                }
            }
        }

        if (Vector3.Distance(transform.position, target.position) <= InfectRange)
        {
            if (HasAddedOneTooAmountOfZombies == false)
            {
                //Jakobs kode +1
                HasAddedOneTooAmountOfZombies = true;
            }
        }
        else
        {
            if (HasAddedOneTooAmountOfZombies == true)
            {
                //Jakobs kode -1
                HasAddedOneTooAmountOfZombies = false;
            }
        }
    }

    void GetNewDirection()
    {
        Vector3 posCloseToPlayer = new Vector3(target.position.x + Random.Range(-15, 16), target.position.y + Random.Range(-15, 16), target.position.z + Random.Range(-15, 16));

        if (NavMesh.SamplePosition(posCloseToPlayer, out NavMeshHit hit, 100, NavMesh.AllAreas))
        {
            WounderingPos = hit.position;

            agent.SetDestination(WounderingPos);
        }
    }

    bool CanSeeTarget()
    {
        Vector3 directionToTarget = target.position - transform.position;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        if (angleToTarget <= fieldOfViewAngle * 0.5f && directionToTarget.magnitude <= detectionRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToTarget.normalized, out hit, detectionRange))
            {
                if (hit.transform == target)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

