using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleStick : MonoBehaviour {
    #region References
    [Header("References")]
    public TruckDriveManager TruckDriveManager;
    public HingeJoint Stick;
    #endregion

    #region Settings
    /// <summary>Conversion factor from angles to throttle</summary>
    [Header("Settings")]
    public float ThrottleScale = 1;
    /// <summary>Dead zone in degrees to not change throttle</summary>
    public float DeadZoneDelta = 4;
    public bool ReverseThrottle = true;
    #endregion

    #region Unity Method
    /// <summary>Checks references</summary>
    public void Awake() {
        if (TruckDriveManager == null) throw new ArgumentNullException();
        if (Stick == null) throw new ArgumentNullException();
    }

    /// <summary>Sends throttle orientation to truck drive manager</summary>
    public void Update() {
        if (Mathf.Abs(Stick.angle) < DeadZoneDelta) {
            TruckDriveManager.Torque = 0;
            return;
        }

        Debug.Log(Stick.angle);
        TruckDriveManager.Torque = Stick.angle * (ReverseThrottle ? -ThrottleScale : ThrottleScale);
    }
        
    #endregion
}
