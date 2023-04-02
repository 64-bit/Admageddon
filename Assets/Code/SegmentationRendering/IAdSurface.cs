using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Admageddon
{
    /// <summary>
    /// Represents a surface that can contain an ad or other content, along with the segmentation color
    /// </summary>
    public interface IAdSurface
    {
        void SetSurfaceContent(Texture surfaceTexture, Color segmentationColor);
    }
}