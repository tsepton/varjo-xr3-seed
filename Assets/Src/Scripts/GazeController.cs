using System;
using UnityEngine;
using Varjo.XR;
using Random = UnityEngine.Random;

public class GazeController : MonoBehaviour {
    public Camera headset;

    private void Update() {
        var data = VarjoEyeTracking.GetGaze();
        var worldPerspective = headset.transform.TransformPoint(data.gaze.origin);
        var worldPerspectiveForward = headset.transform.TransformDirection(data.gaze.forward);
        if (Physics.Raycast(worldPerspective, worldPerspectiveForward, out RaycastHit hit, Mathf.Infinity)) {
            // GameObject being looked at - do something
            UpdateGoColor(hit.collider.gameObject);
        }
    }

    private void UpdateGoColor(GameObject go) {
        try {
            go.GetComponent<Renderer>().material.color = Random.ColorHSV();
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}