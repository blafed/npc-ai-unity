using UnityEngine;
using UnityEngine.AI;
public class Brain : MonoBehaviour
{
    public NPC npc { get; }
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
}