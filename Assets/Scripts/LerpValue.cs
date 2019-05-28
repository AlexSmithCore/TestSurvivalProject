using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LerpValue {

    public static IEnumerator Smooth(Ref<float> reference)
    {
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = reference.WaitFor / (Time.realtimeSinceStartup - startTime);
            float val = Mathf.Lerp(reference.Value, reference.To, fraction);
            reference.Value = val;
            yield return null;
        }
    }

    public static float SmoothRealtime(float value, float to, float waitFor){
        float val = Mathf.Lerp(value, to, waitFor * Time.deltaTime);
        return val;
    }
}

#region ReferenceClass
public class Ref<T>
{
    private T backing;
    public T To;
    public T WaitFor;
    public T Value { get { return backing; } set { backing = value; } }
    public Ref(T reference, T to, T timer)
    {
        backing = reference;
        To = to;
        WaitFor = timer;
    }
}
#endregion