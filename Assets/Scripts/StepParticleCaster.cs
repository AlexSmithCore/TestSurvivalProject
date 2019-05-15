using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepParticleCaster : MonoBehaviour
{

    public bool footContact;

    public float rayLenght;

    void Update(){
        Ray contactRay = new Ray(transform.position , Vector3.down);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        RaycastHit hit;
        if(Physics.Raycast(contactRay, out hit, rayLenght)){
            Debug.Log("enter!");
            footContact = true;
        } else {
            Debug.Log("exit!");
            footContact = false;
        }

        Debug.DrawRay(contactRay.origin, contactRay.direction * rayLenght, Color.red);
    }
}
