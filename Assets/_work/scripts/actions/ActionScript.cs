using UnityEngine;

public class ActionScript : MonoBehaviour
{
    public event System.Action<ActionScript> onStoppedEvent;
    public ActionType type { get; set; }
    public float time => Time.time - this._time;
    float _time;


    public ITarget target { get; set; }


    public void begin(ITarget target)
    {
        enabled = true;
        _time = Time.time;
        this.target = target;

        onBegin();
    }
    public void stop()
    {
        onStopped();
        enabled = false;
        onStoppedEvent?.Invoke(this);
    }
    protected virtual void onBegin() { }
    protected virtual void onStopped() { }
}