using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public enum AgentFraction
{
    Farmer,
    Militia,
    Bandit
}
public class NPCBrain : MonoBehaviour
{
    public AgentFraction agentFraction;
    public Transform targetTransform; // Assign this in the Inspector
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    public float stoppingDistance = 2f;

    private NavMeshAgent agent;
    private NPCBody body;
    private bool isWaiting = false;
    private float waitStartTime;
    private bool isCombat;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        body = GetComponent<NPCBody>();
        SetDestination();
    }

    void Update()
    {
        if (!isWaiting)
        {
            #region When agent has tartget destination
            if (targetTransform != null)
            {
                if (Vector3.Distance(transform.position, targetTransform.position) < stoppingDistance)
                {
                    StopMovement();
                }
                else
                {
                    agent.SetDestination(targetTransform.position);
                }
            }
            #endregion
            #region When agent has random destination
            else if (!agent.pathPending)
            {
                if (agent.remainingDistance < 0.1f)
                {
                    StopMovement();
                }
            }
            #endregion
        }
        else
        {
            if (targetTransform == null)
            {
                if (Time.time - waitStartTime >= Random.Range(minWaitTime, maxWaitTime))
                {
                    isWaiting = false;
                    SetDestination();
                }
            }
        }
        body.Moving(!isWaiting);
    }
    private IEnumerator Test()
    {
        yield return new WaitForSeconds(3);
        targetTransform = null;
        agent.isStopped = false;
    }
    #region Action when reach to target destination
    void StopMovement()
    {
        if (targetTransform != null) // Check if moving towards a target
        {
            // Stop all movement actions
            agent.isStopped = true;

            // Trigger your action here
            Debug.Log("Triggered action");
            if (!isCombat)
            {
                body.Responding();
                StartCoroutine(Test());
                // Set waiting flag
                isWaiting = true;
                waitStartTime = Time.time;
            }
            else
            {
                isWaiting = true;
                body.Combat();
            }

        }
        else // If moving to random location, continue waiting
        {
            isWaiting = true;
            waitStartTime = Time.time;
        }
    }
    #endregion

    #region Keep eye on alerted target 
    private void OnTriggerEnter(Collider other)
    {
        NPCBrain otherNPCBrain = other.GetComponent<NPCBrain>();
        if (otherNPCBrain != null && otherNPCBrain.agentFraction == this.agentFraction)
        {
            isWaiting = false;
            targetTransform = other.transform;
        }
        else if (otherNPCBrain != null && otherNPCBrain.agentFraction != this.agentFraction)
        {
            isWaiting = false;
            targetTransform = other.transform;
            isCombat = true;
        }
    }
    #endregion

    #region Setting up random destination
    void SetDestination()
    {
        if (targetTransform != null)
        {
            if (Vector3.Distance(transform.position, targetTransform.position) > stoppingDistance)
            {
                agent.SetDestination(targetTransform.position);
            }
        }
        else
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(Random.insideUnitSphere * 10, out hit, 10, NavMesh.AllAreas);
            agent.SetDestination(hit.position);
        }
    }
    #endregion
}
