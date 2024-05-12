using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AgentMoveType
{
    RandomInRange,
    Option2,
    Option3
}
public class NPCBrain : MonoBehaviour
{
    public AgentMoveType MoveType = AgentMoveType.RandomInRange;
    private NavMeshAgent agent;
    private Animator animator;

    private float minX = -10f;
    private float maxX = 10f;
    private float minZ = -10f;
    private float maxZ = 10f;
    public float MinX
    {
        get { return minX; }
        set { minX = value; }
    }
    public float MaxX
    {
        get { return maxX; }
        set { maxX = value; }
    }
    public float MinZ
    {
        get { return minZ; }
        set { minZ = value; }
    }
    public float MaxZ
    {
        get { return maxZ; }
        set { maxZ = value; }
    }

    public float changeDestinationInterval = 3f;
    public float holdDuration = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(MoveToRandomDestination());
    }

    IEnumerator MoveToRandomDestination()
    {
        if(MoveType == AgentMoveType.RandomInRange)
        {
            while (true)
            {
                Vector3 randomPosition = new Vector3(Random.Range(MinX, MaxX), 0f, Random.Range(MinZ, MaxZ));
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                    yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
                    yield return new WaitForSeconds(holdDuration);
                }
                else
                {
                    Debug.LogWarning("Random position is not on NavMesh!");
                }
            }
        }
    }

    void Update()
    {
        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("Move", isMoving);
    }
}
