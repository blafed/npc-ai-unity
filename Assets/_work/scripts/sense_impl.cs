using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UnitSense : Sense
{
    public override float weight(MindType other, ICurrentParams current, ITarget newTarget)
    {

        if (other == current.currentMind)
            return 1f / current.time;
        switch (other)
        {
            case MindType.idle: return 0.5f;
            case MindType.afraid: return 1;
        }

        return 0;

    }
}
[System.Serializable]
public class PersonObsInSense : Sense
{
    public override float weight(MindType other, ICurrentParams current, ITarget newTarget)
    {
        switch (other)
        {
            case MindType.low_tension: return 0.25f;
            case MindType.aggressive: return Random.Range(0, 0.2f);
        }
        return 0;
    }
}
public class PersonObsOutSense : Sense
{
    public override float weight(MindType other, ICurrentParams current, ITarget newTarget)
    {
        if (newTarget == current.target)
            switch (current.currentMind)
            {
                case MindType.aggressive:
                case MindType.want_to_bully:
                case MindType.afraid:
                    if (other == MindType.idle || other == MindType.low_tension)
                        return Random.Range(0, 0.5f);
                    break;
            }
        switch (other)
        {

            case MindType.low_tension: return Random.Range(0.25f, 0.4f);
            case MindType.high_tension: return Random.Range(0, 0.35f);
            case MindType.afraid: return 0.1f;
            case MindType.idle: return Random.Range(0.1f, 0.5f);
        }
        return 0;
    }
}
public class GotDamagedSense : Sense
{
    public override SenseType type => SenseType.got_damaged;
    public override float weight(MindType other, ICurrentParams current, ITarget newTarget)
    {
        switch (other)
        {
            case MindType.self_defense:
                return Random.Range(0.5f, 1f);
            case MindType.aggressive:
            case MindType.afraid:
                return Random.Range(0.5f, 0.8f);
        }
        return 0;
    }
}
public class HungrySense : Sense
{
    public override SenseType type => SenseType.hungry;
    public override float weight(MindType other, ICurrentParams current, ITarget newTarget)
    {
        switch (other)
        {
            case MindType.wants_food:
                if (newTarget != null && newTarget.type == TargetType.food)
                    return 1;
                else return 0.5f;
        }
        return base.weight(other, current, newTarget);
    }
}
public class ThristSense : Sense
{
    public override SenseType type => SenseType.hungry;

    public override float weight(MindType other, ICurrentParams current, ITarget newTarget)
    {
        switch (other)
        {
            case MindType.wants_drink:
                if (newTarget != null && newTarget.type == TargetType.drink)
                    return 1;
                else return 0.5f;
        }
        return base.weight(other, current, newTarget);
    }
}
public class BoughtSomethingSense : Sense
{
    public override SenseType type => SenseType.bought_something;
    public override float weight(MindType other, ICurrentParams current, ITarget newTarget)
    {
        switch (other)
        {
            case MindType.wants_food:
            case MindType.wants_drink:
            case MindType.idle:
                return Random.value;
        }
        return 0;
    }
}
public class SelledSomethingSense : Sense
{
    public override float weight(MindType other, ICurrentParams current, ITarget newTarget)
    {
        switch (other)
        {
            case MindType.want_to_sell:
                return 1f;
        }
        return 0;
    }
}



public class BasicSenseFilter : SenseFilter
{
    public float rank(SenseType type)
    {
        switch (type)
        {
            case SenseType.unit: return 0;
            case SenseType.got_damaged: return 1;
            case SenseType.hungry: return 0.7f;
            case SenseType.thrist: return 0.75f;
            case SenseType.selled_something: return 0.35f;
            case SenseType.person_observed_in: return 0.45f;
            case SenseType.person_ovserved_out: return 0.45f;
            case SenseType.bought_something: return 0.5f;
            default: return 0;
        }
    }
    public override SenseData select(IEnumerable<SenseData> senseDatas)
    {
        SenseData sd = default(SenseData);
        float rank = 0;
        foreach (var x in senseDatas)
        {
            var r = this.rank(x.type);
            if (r > rank)
            {
                rank = r;
                sd = x;
            }
        }
        return sd;
    }
}