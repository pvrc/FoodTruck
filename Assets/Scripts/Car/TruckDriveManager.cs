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
        get => FrontWheels[0].transform.localEulerAngles.y;
        set {
            var angle = Mathf.Clamp(value, -MaxSteeringDelta, MaxSteeringDelta);
            foreach (var w in FrontWheels)
                w.transform.localEulerAngles = new Vector3(0, angle, 0);
        }
    }
    #endregion

    #region Settings
    /// <summary> Max delta in degrees to deviate from steering </summary>
    [Range(0, 180), Tooltip("Max delta in degrees to deviate from steering"),
    Header("Settings")]
    public float MaxSteeringDelta = 75;

    /// <summary>Steering wheel offset in degrees</summary>
    [Range(0, 360), Tooltip("Steering wheel offset in degrees")]
    public float SteeringWheelOffset = 270;

    /// <summary>Factor to break when throttle is 0</summary>
    public float BrakeFactor = 10;

    public float MaxTorque = 15;
    #endregion

    #region References
    [Header("References")]
    public List<WheelCollider> FrontWheels = new List<WheelCollider>();
    public List<WheelCollider> BackWheels = new List<WheelCollider>();
    #endregion

    #region Vars
    protected List<WheelCollider> _allWheels = null;
    protected List<WheelCollider> AllWheels {
        get {
            if (_allWheels == null) {
                _allWheels = new List<WheelCollider>();
                _allWheels.AddRange(FrontWheels);
                _allWheels.AddRange(BackWheels);
            }
            return _allWheels;
        }
    }
    #endregion

    #region Unity Methods
    /// <summary>Check initial references</summary>
    protected void Awake() {
        if (FrontWheels == null) throw new ArgumentNullException();
        if (BackWheels == null) throw new ArgumentNullException();
        foreach (var w in FrontWheels)
            if (w == null) throw new ArgumentNullException();
        foreach (var w in BackWheels)
            if (w == null) throw new ArgumentNullException();

        if (FrontWheels.Count != 2) throw new ArgumentException();
        if (BackWheels.Count != 2) throw new ArgumentException();
    }

    /// <summary>Sets torque on all wheels</summary>
    protected void Update() {
        if (Mathf.Abs(Torque) > Mathf.Epsilon) {
            foreach (var w in AllWheels) {
                w.brakeTorque = 0;
                w.motorTorque = Torque;
            }
        } else {
            foreach (var w in AllWheels) {
                w.motorTorque = 0;
                w.brakeTorque = Mathf.Abs(BrakeFactor);
            }
        }
    }
    #endregion
}
