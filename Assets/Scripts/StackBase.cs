using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBase : MonoBehaviour
{
    public BoxCollider objTrigger;
    private Vector3 stackHeight;
    private Vector3 original;

    // Start is called before the first frame update
    void Start()
    {
        stackHeight = Vector3.zero;
        original = objTrigger.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Stackable") return;
        //Debug.Log("Collision");
        Transform t = other.transform;
        StackableObject otherScript = other.GetComponent<StackableObject>();
        //if (otherScript.GetType() != typeof(IStackable)) return;
        otherScript.IsDisabled = false;
        t.parent = transform;
        t.localEulerAngles = otherScript.Euler;
        t.localPosition = stackHeight + otherScript.Offset;
        stackHeight += otherScript.Offset;
        UpdateColliders(otherScript.Offset);
        
    }

    private void UpdateColliders(Vector3 offset)
    {
        objTrigger.size += offset;
        objTrigger.center += offset / 2;
    }
}
