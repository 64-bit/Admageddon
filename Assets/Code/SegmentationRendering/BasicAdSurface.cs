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
        private static int _backgroundColorId = Shader.PropertyToID("_BackgroundColor");

        /// <summary>
        /// This should be set to the same index as the mesh renderer material that should display the surface content
        /// </summary>
        public int AdSurfaceMaterialIndex;
        
        
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            var materials = _meshRenderer.materials;
            
            if (AdSurfaceMaterialIndex >= materials.Length)
            {
                Debug.LogError("AdSurfaceMaterialIndex is out of range");
            }
        }

        public void SetSurfaceContent(Texture surfaceTexture, Color segmentationColor)
        {
            var materials = _meshRenderer.materials;
            
            materials[AdSurfaceMaterialIndex].SetTexture("_MainTex", surfaceTexture);
            materials[AdSurfaceMaterialIndex].SetSegmentationColor(segmentationColor);
            //Set the background to a random color with solid alpha
            materials[AdSurfaceMaterialIndex].SetColor(_backgroundColorId, new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f));
            
            _meshRenderer.materials = materials;
        }
    }
}

