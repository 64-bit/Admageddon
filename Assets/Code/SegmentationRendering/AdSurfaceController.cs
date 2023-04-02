using System.Collections;
using System.Collections.Generic;
using Admageddon;
using UnityEngine;

public class AdSurfaceController : MonoBehaviour
{
    public static readonly Color DefaultSegmentationColor = Color.black;
    public static readonly Color AdSegmentationColor = Color.white; 
    
    // Start is called before the first frame update
    public List<Texture> Ads;
    
    void Start()
    {
        ReRollAllAds();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ReRollAllAds();
        }
    }

    public void ReRollAllAds()
    {
        foreach (var adSurface in GetComponentsInChildren<IAdSurface>())
        {
            adSurface.SetSurfaceContent(Ads[Random.Range(0, Ads.Count)], AdSegmentationColor);
        }
    }
}