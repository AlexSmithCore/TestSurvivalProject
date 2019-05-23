using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothValueChanger : MonoBehaviour
{
    float result;

    public float SmoothChangeTo(float from, float to, float time){
        result = from;
        StartCoroutine(Change(from,to,time));
        return result;
    }

    IEnumerator Change(float from, float to,float t){
        while(from < to){
            result = Mathf.Lerp(from, to, t);
            yield return null;
        }
    }
}
