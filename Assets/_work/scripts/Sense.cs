using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SenseType
{
    unit,
    person_observed_in,
    person_ovserved_out,
    got_damaged,
    person_aiming_at_me,
    got_shooted,

    hungry,
    thrist,
    bought_something,
    selled_something,

    COUNT,

}
public enum ActionType
{

    standing,
    eating,
    drinking,
    random_walk,
    arguing,
    attacking,
    run_to,
    chasing,
    walk_to,
    selling,
    buying,
    run_away,


    COUNT,

}
public enum MindType
{
    idle,
    low_tension,
    high_tension,
    self_defense,
    aggressive,
    wants_food,
    wants_drink,
    wants_revenge,
    afraid,
    want_to_bully,
    want_to_sell,
    want_to_buy,

    COUNT,
}
public interface ITarget
{

    Vector3? point { get; }
    GameObject gameObject { get; }
    Transform transform { get; }
    TargetType type { get; }
}
public enum TargetType
{
    unknown,
    person,
    point,
    food,
    drink
}

public interface ICurrentParams
{
    ITarget target { get; }
    float dst { get; }
    float time { get; }
    MindType currentMind { get; }
    bool isActionFinished { get; }
}
public struct SenseData
{
    public SenseType type;
    public ITarget target;
}
public class SenseFilter
{
    public virtual SenseData select(IEnumerable<SenseData> senses)
    {
        foreach (var x in senses)
            return x;
        return new SenseData();
    }
}

public class Sense
{
    public virtual SenseType type { get; }
    public virtual float weight(MindType other, ICurrentParams current, ITarget newTarget)
    {
        return 0;
    }

    public static Sense create(SenseType o)
    {
        switch (o)
        {
            case SenseType.unit: return new UnitSense();
        }
        return new Sense();
    }


}

public class Mind
{
    public virtual MindType type { get; }
    public virtual float weight(MindType other, ICurrentParams current)
    {
        return 0;
    }
    public virtual float weight(ActionType other, ICurrentParams current)
    {
        return 0;
    }


    public static Mind create(MindType o)
    {
        switch (o)
        {
            case MindType.idle: return new IdleMind();
            case MindType.low_tension: return new LowTensionMind();
        }
        return new Mind();
    }
}

public class Action
{
    public bool enabled => this.script && this.script.enabled;
    public ActionScript script { get; }
    Action(ActionType t, ActionScript mono)
    {
        this.type = t;
        this.script = mono;
    }
    public virtual ActionType type { get; }


    static T createOrGet<T>(GameObject go) where T : Component
    {
        T t;
        if (go.TryGetComponent(out t))
            return t;
        return go.AddComponent<T>();
    }

    public virtual float weight(ActionType other, ICurrentParams current)
    {
        return 1;
    }

    public static Action create(GameObject go, ActionType o)
    {
        ActionScript mono = null;
        switch (o)
        {
            case ActionType.random_walk:
                mono = createOrGet<WalkRandomly>(go);
                break;
            case ActionType.standing:
                mono = createOrGet<Standing>(go);
                break;

        }
        if (mono
        )
        {
            mono.type = o;
            mono.enabled = false;
        }

        return new Action(o, mono);
    }

}

public static class Extensions
{
    public static float dst(this ITarget target, Vector3 p)
    {
        return Vector3.Distance(target.pos(), p);
    }
    public static Vector3 pos(this ITarget target)
    {
        if (target.point != null) return target.point.Value;
        if (target.transform) return target.transform.position;
        return Vector3.zero;
    }

}