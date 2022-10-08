using UnityEngine;
using System.Collections.Generic;
public class NPC : MonoBehaviour, ICurrentParams
{

    public SenseFilter senseFilter;

    public List<Sense> senses = new List<Sense>();
    public List<Mind> minds = new List<Mind>();
    public List<Action> actions = new List<Action>();


    public ITarget target { get; set; }
    public float dst { get; }
    public float time => Time.time - _mindtime;

    public ActionType currentAction { get; set; }
    public MindType currentMind { get; set; }
    public bool isActionFinished => !actions[(int)currentAction].enabled;

    float _mindtime;



    private void Start()
    {
        senseFilter = new BasicSenseFilter();
        for (SenseType s = 0; s < SenseType.COUNT; s++)
        {
            senses.Add(Sense.create(s));
        }
        for (MindType s = 0; s < MindType.COUNT; s++)
        {
            minds.Add(Mind.create(s));
        }
        for (ActionType s = 0; s < ActionType.COUNT; s++)
        {
            actions.Add(Action.create(gameObject, s));
        }
    }

    public List<SenseData> inputSenses = new List<SenseData>();

    IEnumerable<SenseData> builtSenses()
    {
        if (inputSenses != null)
            foreach (var x in inputSenses)
                yield return x;
        yield return new SenseData { type = SenseType.unit };
    }
    private void FixedUpdate()
    {
        onSenses(builtSenses());
        inputSenses.Clear();
    }

    public void startAction(ActionType type)
    {
        if (currentAction != type || !actions[(int)currentAction].enabled)
        {
            actions[(int)type].script.begin(this.target);
            currentAction = type;
        }
    }

    public void onSenses(IEnumerable<SenseData> senseDatas)
    {
        var selected = senseFilter.select(senseDatas);
        var sense = senses[(int)selected.type];
        var weight = 0f;
        Mind mind = null;
        foreach (var x in minds)
        {

            float w = sense.weight(x.type, this, selected.target);
            if (weight < w)
            {
                mind = x;
                weight = w;
            }
        }

        if (mind != null)
        {
            weight = 0;
            target = selected.target;
            Action foundAction = null;
            Mind foundMind = null;

            foreach (var x in minds)
            {
                float w = mind.weight(x.type, this);
                if (w > weight)
                {
                    foundMind = mind;
                    weight = w;
                }
            }

            weight = 0;
            mind = foundMind;


            foreach (var x in actions)
            {

                float w = mind.weight(x.type, this);
                if (w > weight)
                {
                    weight = w;
                    foundAction = x;
                }
            }

            currentMind = mind.type;
            if (foundAction != null)
                startAction(foundAction.type);
        }

        if (mind != null && mind.type != this.currentMind)
        {
            this.currentMind = mind.type;
            _mindtime = 0;
        }
        _mindtime = Time.time;

    }

}