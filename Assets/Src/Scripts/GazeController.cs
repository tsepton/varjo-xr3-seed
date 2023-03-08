using UnityEngine;
using Varjo.XR;

public class GazeController : MonoBehaviour
{
    private void Update()
    {
        var data = VarjoEyeTracking.GetGaze();
        Debug.Log(data.gaze.origin);
        Debug.Log(data.gaze.forward * 100);
        Debug.DrawRay(data.gaze.origin, data.gaze.forward * 100,  Color.green);
    }
}
