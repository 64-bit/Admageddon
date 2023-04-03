using UnityEngine;

namespace Admageddon
{
    [RequireComponent(typeof(Camera))]
    public class RenderTextureOutput : MonoBehaviour
    {
        public Camera SourceCamera;
        public Camera SegmentationCamera;
        
        public EDisplayMode DisplayMode;
        public Material SplitScreenMaterial;
        private static readonly int SplitTex = Shader.PropertyToID("_SplitTex");

        void Start()
        {
            if (SourceCamera == null)
            {
                Debug.LogError("Source camera is not set. Please assign a camera with a Render Texture.");
                enabled = false;
                return;
            }

            if (SourceCamera.targetTexture == null)
            {
                Debug.LogError("Source camera must have a Render Texture assigned.");
                enabled = false;
                return;
            }
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            switch (DisplayMode)
            {
                case EDisplayMode.Source:
                    Graphics.Blit(SourceCamera.targetTexture, dest);
                    break;
                case EDisplayMode.Segmentation:
                    Graphics.Blit(SegmentationCamera.targetTexture, dest);
                    break;
                case EDisplayMode.SplitScreen:
                    SplitScreenMaterial.SetTexture(SplitTex, SegmentationCamera.targetTexture);
                    Graphics.Blit(SourceCamera.targetTexture, dest, SplitScreenMaterial);
                    break;
            }

        }
    }

    public enum EDisplayMode
    {
        Source,
        Segmentation,
        SplitScreen
    }
}