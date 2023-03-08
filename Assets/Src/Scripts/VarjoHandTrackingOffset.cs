using UnityEngine;
using UnityEngine.XR;
using Leap.Unity;

[RequireComponent(typeof(LeapXRServiceProvider))]
public class VarjoHandTrackingOffset : MonoBehaviour
{
    private InputDevice _hmd;
    private LeapXRServiceProvider _xrServiceProvider;

    void Start()
    {
        _hmd = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        _xrServiceProvider = GetComponent<LeapXRServiceProvider>();

        switch (_hmd.name)
        {
            case "XR-3":
            case "VR-3":
                _xrServiceProvider.deviceOffsetMode = LeapXRServiceProvider.DeviceOffsetMode.ManualHeadOffset;
                _xrServiceProvider.deviceOffsetYAxis = -0.0112f;
                _xrServiceProvider.deviceOffsetZAxis = 0.0999f;
                _xrServiceProvider.deviceTiltXAxis = 0f;
                break;
            case "VR-2 Pro":
                _xrServiceProvider.deviceOffsetMode = LeapXRServiceProvider.DeviceOffsetMode.ManualHeadOffset;
                _xrServiceProvider.deviceOffsetYAxis = -0.025734f;
                _xrServiceProvider.deviceOffsetZAxis = 0.068423f;
                _xrServiceProvider.deviceTiltXAxis = 5f;
                break;
            default:
                Debug.Log("Error detecting headset");
                break;
        }
    }
}