using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Admageddon
{
    /// <summary>
    /// This component sets the segmentation color of all materials on the mesh renderer of this object
    /// </summary>
    public class StaticSegmentationColor : MonoBehaviour
    {
        /// <summary>
        /// Set all materials on the mesh renderer to have this segmentation color
        /// </summary>
        public Color SegmentationColor = Color.white;

        /// <summary>
        /// If the number of colors in this list is equal to or greater than the number of materials on the mesh renderer, then the colors will be applied to the materials in the same order
        /// </summary>
        public List<Color> PerMaterialColors;

        private void Awake()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                return;
            }

            var materials = meshRenderer.materials;

            if (PerMaterialColors.Count >= materials.Length)
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i].SetSegmentationColor(PerMaterialColors[i]);
                }

                meshRenderer.materials = materials;
            }
            else
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i].SetSegmentationColor(SegmentationColor);
                }

                meshRenderer.materials = materials;
            }
        }
    }
}