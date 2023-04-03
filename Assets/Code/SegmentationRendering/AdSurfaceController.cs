using System.Collections;
using System.Collections.Generic;
using Admageddon;
using UnityEngine;
using System.Linq;

public class AdSurfaceController : MonoBehaviour
{
    public static readonly Color DefaultSegmentationColor = Color.black;
    public static readonly Color AdSegmentationColor = Color.white; 
    
    // Start is called before the first frame update
    public List<Texture> Ads;
    public List<Texture> NotAds;

    public float AdFraction = 0.75f;
    public float DropoutEnabledFraction = 0.75f;

    private IEnumerable<IAdSurface> _adCache;
    private IEnumerable<IDropout> _dropouts;

    void Awake()
    {
        _adCache = FindObjectsOfType<GameObject>().SelectMany(go => go.GetComponents<IAdSurface>()).ToList();
        _dropouts = FindObjectsOfType<GameObject>().SelectMany(go => go.GetComponents<IDropout>()).ToList();
    }
    
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
        foreach (var obj in _dropouts)
        {
            obj.SetIsEnabled(Random.value < DropoutEnabledFraction);
        }
        
        foreach (var adSurface in _adCache)
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