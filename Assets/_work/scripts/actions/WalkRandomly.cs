using UnityEngine;
using UnityEngine.AI;

public class WalkRandomly : ActionScript
{


    public float moveAroundRadius = 5f;
    public float speedMuliplier = 0.5f;
    public NavMeshAgent agent;


    Vector3 targetMovement;

    private void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        var v = Random.insideUnitSphere;
        v.y = 0;
        targetMovement = transform.position + v * moveAroundRadius;
    }

    private void FixedUpdate()
    {
        agent.speed = 2f;
        if (agent.SetDestination(targetMovement) || time > 10)
        {
            stop();
        }
    }

}