using UnityEngine;
public class IdleMind : Mind
{
    public override MindType type => MindType.idle;
    public override float weight(MindType other, ICurrentParams current)
    {

        switch (other)
        {
            case MindType.idle: return 1 / current.time;
            case MindType.low_tension: return current.time / 10f;
        }
        return 0;
    }
    public override float weight(ActionType other, ICurrentParams current)
    {
        if (current.isActionFinished)
            return 0;
        switch (other)
        {
            case ActionType.random_walk: return Random.Range(0.25f, 1);
            case ActionType.standing: return Random.Range(0, 1);

        }
        return base.weight(other, current);
    }
}
public class LowTensionMind : Mind
{
    public override MindType type => MindType.low_tension;
    public override float weight(MindType other, ICurrentParams current)
    {
        float f = 0.25f;
        if (!current.isActionFinished)
            f = 1;
        switch (other)
        {
            case MindType.low_tension: return 1 / current.time * f;
            case MindType.idle: return current.time / 10f * f;
        }
        return 0;
    }
    public override float weight(ActionType other, ICurrentParams current)
    {

        switch (other)
        {
            case ActionType.random_walk: return Random.Range(0, 1);
            case ActionType.standing: return Random.Range(0.25f, 1);
        }
        return base.weight(other, current);
    }
}
public class AgressiveMind : Mind
{
    public override MindType type => MindType.aggressive;


    public override float weight(ActionType other, ICurrentParams current)
    {
        switch (other)
        {
            case ActionType.attacking: return current.time / 7f;


        }
        return 0;
    }
    public override float weight(MindType other, ICurrentParams current)
    {
        return base.weight(other, current);
    }
}
public class HighTensionMind : Mind
{
    public override MindType type => MindType.high_tension;
    public override float weight(MindType other, ICurrentParams current)
    {
        switch (other)
        {
            case MindType.low_tension:
                return current.time / 5f;
            case MindType.aggressive:
            case MindType.self_defense:
            case MindType.afraid:
                if (current.target != null && current.target.type == TargetType.person)
                    return current.time / 3f * Random.Range(0.8f, 2);
                break;
        }
        return 0;
    }
    public override float weight(ActionType other, ICurrentParams current)
    {
        switch (other)
        {

        }
        return 1;
    }
}

public class WantsFoodMind : Mind { }
public class WantsDrinkMind : Mind { }
public class WantsRevengeMind : Mind { }
public class AfraidMind : Mind { }
public class WantToBullyMind : Mind { }
public class WantToSellMind : Mind { }