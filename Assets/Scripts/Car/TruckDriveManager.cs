using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckDriveManager : MonoBehaviour {
    #region Properties
    [SerializeField, Header("Properties")]
    protected float _torque;
    public float Torque {
        get => _torque;
        set => _torque = value;
    }

    /// <summary>Deviation in degrees to steer from front</summary>
    public float SteeringAngle {
        get => wheelCollider[0].steerAngle;
        set {
            var angle = Mathf.Clamp(value, -MaxSteeringDelta, MaxSteeringDelta);
            foreach (var w in wheelCollider)
                w.steerAngle = angle;
        }
    }
    #endregion

    #region Settings
    /// <summary> Max delta in degrees to deviate from steering </summary>
    [Range(0, 180), Tooltip("Max delta in degrees to deviate from steering"),
    Header("Settings")]
    public float MaxSteeringDelta = 75;

    public float MaxTorque = 15;
    #endregion

    #region References
    [Header("References")]
    public WheelCollider[] wheelCollider;
    #endregion

    #region Unity Methods
    /// <summary>Check initial references</summary>
    protected void Awake() {
        if (wheelCollider == null) throw new ArgumentNullException();
        foreach (var w in wheelCollider)
            if (w == null) throw new ArgumentNullException();

        if (wheelCollider.Length != 4) throw new ArgumentOutOfRangeException();
    }

    protected void Update() {
        // Set torque on all wheels
        if (Torque > 0) {
            foreach (var w in wheelCollider) {
                w.brakeTorque = 0;
                w.motorTorque = Torque;
            }
        } else {
            foreach (var w in wheelCollider) {
                w.motorTorque = 0;
                w.brakeTorque = -Torque;
            }
        }
    }
    #endregion
}
