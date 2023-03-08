using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Varjo.XR;

public class GazeCalibrator : MonoBehaviour
{
    private bool _currentlyCalibrating;
    private VarjoEyeTracking.GazeCalibrationQuality _calibrationQuality;

    // Lifecycle methods
    private void Start()
    {
        InvokeRepeating(nameof(RequestCalibrationIfNeeded), 2.0f, 20f);
    }


    // Private methods
    private void RequestCalibrationIfNeeded()
    {
        _calibrationQuality = VarjoEyeTracking.GetGazeCalibrationQuality();
        Debug.Log($"right {_calibrationQuality.right} left{_calibrationQuality.left}");
        var leftCalibrationValid = _calibrationQuality.left >= VarjoEyeTracking.GazeEyeCalibrationQuality.Medium;
        var rightCalibrationValid = _calibrationQuality.right >= VarjoEyeTracking.GazeEyeCalibrationQuality.Medium;
        if (leftCalibrationValid && rightCalibrationValid) return;
        
        bool ok = VarjoEyeTracking.RequestGazeCalibration(VarjoEyeTracking.GazeCalibrationMode.Fast,
            VarjoEyeTracking.HeadsetAlignmentGuidanceMode.AutoContinueOnAcceptableHeadsetPosition);
        if (!ok) RequestCalibrationIfNeeded();
    }
}