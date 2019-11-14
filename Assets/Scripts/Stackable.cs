using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable
{
    Vector3 Offset { get; }
    Vector3 Euler { get; }
    bool IsDisabled { get; set; }
}
