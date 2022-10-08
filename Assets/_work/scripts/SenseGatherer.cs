using UnityEngine;

public class SenseGatherer : MonoBehaviour
{
    NPC npc;

    private void Start()
    {
        npc = GetComponentInParent<NPC>();
    }
    private void OnTriggerEnter(Collider other)
    {

        Projectile proj;
        PersonTarget target;

        if (other.TryGetComponent(out target))
        {
            npc.inputSenses.Add(new SenseData { type = SenseType.person_observed_in, target = target });
        }
        else if (other.TryGetComponent(out proj))
        {
            npc.inputSenses.Add(new SenseData { type = SenseType.got_damaged, target = proj.sender });
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PersonTarget target;

        if (other.TryGetComponent(out target))
        {
            npc.inputSenses.Add(new SenseData { type = SenseType.person_ovserved_out, target = target });
        }
    }

}