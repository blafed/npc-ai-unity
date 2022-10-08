using UnityEngine;

public class PersonTarget : MonoBehaviour, ITarget
{
    public Vector3? point => null;

    public TargetType type => TargetType.person;
}