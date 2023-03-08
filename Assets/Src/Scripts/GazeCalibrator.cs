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
        InvokeRepeating(nameof(RequestCalibrationIfNeeded), 5f, 5f);
    }


    // Private methods
    private void RequestCalibrationIfNeeded()
    {
        var gaze = VarjoEyeTracking.GetGaze();
        if (gaze.status == VarjoEyeTracking.GazeStatus.Adjust) return;
        
        _calibrationQuality = VarjoEyeTracking.GetGazeCalibrationQuality();
        var leftCalibrationValid = _calibrationQuality.left >= VarjoEyeTracking.GazeEyeCalibrationQuality.Medium;
        var rightCalibrationValid = _calibrationQuality.right >= VarjoEyeTracking.GazeEyeCalibrationQuality.Medium;
        if (leftCalibrationValid && rightCalibrationValid) return;
        
        bool ok = VarjoEyeTracking.RequestGazeCalibration(VarjoEyeTracking.GazeCalibrationMode.OneDot,
            VarjoEyeTracking.HeadsetAlignmentGuidanceMode.AutoContinueOnAcceptableHeadsetPosition);
        if (!ok) RequestCalibrationIfNeeded();
    }
}