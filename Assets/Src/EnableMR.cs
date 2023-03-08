using UnityEngine;
using Varjo.XR;

public class EnableMR : MonoBehaviour
{
    public bool videoSeeThrough = true;
    public bool depthEstimation = true;

    // VST related
    private Camera _camera;
    private Color _originalCameraColor;
    private readonly Color _arCameraColor = new Color(0.0f, 0.0f, 0.0f, 0f);
    
    // Depth of field related
    private bool _originalSubmitDepthValue = false;
    private bool _originalDepthSortingValue = false;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        _originalCameraColor = _camera.backgroundColor;
        
        if (videoSeeThrough) EnableVst();
        else DisableVst();
        
        if (depthEstimation) EnableDepthEstimation();
        else DisableDepthEstimation();
    }

    private void EnableVst()
    {
        _camera.clearFlags = CameraClearFlags.SolidColor;
        _camera.backgroundColor = _arCameraColor;
        VarjoMixedReality.StartRender();
    }

    private void DisableVst()
    {
        _camera.clearFlags = CameraClearFlags.Skybox;
        _camera.backgroundColor = _originalCameraColor;
        VarjoMixedReality.StopRender();
    }

    private void EnableDepthEstimation()
    {
        depthEstimation = VarjoMixedReality.EnableDepthEstimation();
        _originalSubmitDepthValue = VarjoRendering.GetSubmitDepth();
        _originalDepthSortingValue = VarjoRendering.GetDepthSorting();
        VarjoRendering.SetSubmitDepth(true);
        VarjoRendering.SetDepthSorting(true);
    }

    private void DisableDepthEstimation()
    {
        VarjoMixedReality.DisableDepthEstimation();
        VarjoRendering.SetSubmitDepth(_originalSubmitDepthValue);
        VarjoRendering.SetDepthSorting(_originalDepthSortingValue);
    }
}