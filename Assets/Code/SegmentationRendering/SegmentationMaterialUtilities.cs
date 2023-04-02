using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Admageddon
{
    public static class SegmentationMaterialUtilities
    {
        private static int _segmentationId = Shader.PropertyToID("_SegmentationColor");

        public static void SetSegmentationColor(this Material material, Color color)
        {
            material.SetColor(_segmentationId, color);
        }
    }
}