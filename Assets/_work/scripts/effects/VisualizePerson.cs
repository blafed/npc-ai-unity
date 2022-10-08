using UnityEngine;
using System.Collections.Generic;
public class VisualizePerson : MonoBehaviour
{

    NPC npc;
    MeshRenderer renderer;

    MindType? lastMindType;

    private void Start()
    {
        npc = GetComponentInParent<NPC>();
        renderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (lastMindType != npc.currentMind)
            renderer.material = materials[(int)npc.currentMind];
        lastMindType = npc.currentMind;
    }
    public Material[] materials;
}