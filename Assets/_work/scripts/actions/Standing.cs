using UnityEngine;

public class Standing : ActionScript
{
    private void OnEnable()
    {

    }


    protected override void onBegin()
    {
        targetRot = null;
    }

    Quaternion? targetRot;


    private void FixedUpdate()
    {
        if (time > 3)
            enabled = false;
        float factor = 1;
        if (target != null)
        {
            var pos = target.pos();
            var dir = pos - transform.position;
            dir.y = 0;
            Quaternion.LookRotation(dir.normalized, Vector3.up);
            factor = Random.Range(2, 3);
        }
        else
        {
            if (Random.value < Time.fixedDeltaTime)
                targetRot = Quaternion.Euler(0, Random.value * 360, 0);
        }
        if (targetRot.HasValue)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot.Value, factor * Time.fixedDeltaTime);
    }
}