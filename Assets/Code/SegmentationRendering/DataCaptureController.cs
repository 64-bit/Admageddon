using Admageddon;
using UnityEngine;

public class DataCaptureController : MonoBehaviour
{
    public FrameGrabber TargetGrabber;
    public AdSurfaceController AdSurfaceController;

    public bool CaptureEnabled = false;
    
    public int FrameCount = 200;
    public float FrameTimeStep = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (CaptureEnabled && TargetGrabber != null)
        {
            TargetGrabber.PreGrabFrame += AdSurfaceController.ReRollAllAds;
            TargetGrabber.StartCapture(FrameCount, FrameTimeStep);
        }
    }
}
