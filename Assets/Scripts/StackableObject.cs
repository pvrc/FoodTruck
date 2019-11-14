using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableObject : MonoBehaviour, IStackable
{
    [SerializeField]
    protected Vector3 _offset;
    public Vector3 Offset {
        get => _offset;
        protected set
        {
            _offset = value;
        }
    }

    [SerializeField]
    protected Vector3 _euler;
    public Vector3 Euler
    {
        get => _euler;
        protected set
        {
            _euler = value;
        }
    }

    protected bool _isDisabled;
    public bool IsDisabled
    {
        get => _isDisabled;
        set
        {
            if (_isDisabled = value)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                Collider[] colliders = gameObject.GetComponents<Collider>();
                foreach (Collider x in colliders)
                {
                    x.enabled = true;
                }
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                Collider[] colliders = gameObject.GetComponents<Collider>();
                foreach (Collider x in colliders)
                {
                    x.enabled = false;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
