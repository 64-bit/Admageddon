using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Admageddon
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BasicAdSurface : MonoBehaviour , IAdSurface
    {
        // Start is called before the first frame update
        private MeshRenderer _meshRenderer;
        private Material[] _localMaterials;

        /// <summary>
        /// This should be set to the same index as the mesh renderer material that should display the surface content
        /// </summary>
        public int AdSurfaceMaterialIndex;
        
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _localMaterials = _meshRenderer.materials;
            
            if (AdSurfaceMaterialIndex >= _localMaterials.Length)
            {
                Debug.LogError("AdSurfaceMaterialIndex is out of range");
            }
        }

        public void SetSurfaceContent(Texture surfaceTexture, Color segmentationColor)
        {
            _localMaterials[AdSurfaceMaterialIndex].SetTexture("_MainTex", surfaceTexture);
            _localMaterials[AdSurfaceMaterialIndex].SetSegmentationColor(segmentationColor);
            _meshRenderer.materials = _localMaterials;
        }
    }
}

