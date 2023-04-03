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
    public List<Texture> NotAds;

    public float AdFraction = 0.75f;
    
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
            SetAdSurface(adSurface);
        }
    }
    
    private void SetAdSurface(IAdSurface adSurface)
    {
        bool isAd = Random.value < AdFraction;
        if (isAd)
        {
            adSurface.SetSurfaceContent(Ads[Random.Range(0, Ads.Count)], AdSegmentationColor);
        }
        else
        {
            adSurface.SetSurfaceContent(NotAds[Random.Range(0, NotAds.Count)], DefaultSegmentationColor);
        }
    }
}