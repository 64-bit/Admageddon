using System.Collections;
using System.IO;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Admageddon
{
    public class FrameGrabber : MonoBehaviour
    {
        public Camera VisualCamera;
        public Camera SegmentationCamera;

        public RenderTexture VisualTexture;
        public RenderTexture SegmentationTexture;

        public int Width = 1024;
        public int Height = 768;

        public int MaxFrames = 20;
        public string DataDirectory = "C:\\Data\\ML";

        public float TimeStep = 0.25f;

        private DirectoryInfo BaseDir;

        private DirectoryInfo MainDir;
        private DirectoryInfo ReplacementDir;

        // Start is called before the first frame update
        void Start()
        {
            VisualTexture = new RenderTexture(Width, Height, 32);
            SegmentationTexture = new RenderTexture(Width, Height, 32);

            BaseDir = new DirectoryInfo(DataDirectory);
            MainDir = BaseDir.CreateSubdirectory("Main");
            ReplacementDir = BaseDir.CreateSubdirectory("Replacement");

            VisualCamera.targetTexture = VisualTexture;
            SegmentationCamera.targetTexture = SegmentationTexture;

            StartCoroutine(CaptureProcess(MaxFrames, TimeStep));
        }

        IEnumerator CaptureProcess(int count, float timeStep)
        {
            for (int i = 0; i < count; i++)
            {
                GetImagesForFrame(i);
                Debug.Log($"Frame Remainin:{count - i}");
                yield return new WaitForSeconds(timeStep);
            }
        }

        private void GetImagesForFrame(int frame)
        {
            AsyncGPUReadback.Request(VisualTexture, 0, request => GetImage(request, VisualTexture, frame, MainDir));
            AsyncGPUReadback.Request(SegmentationTexture, 0,
                request => GetImage(request, SegmentationTexture, frame, ReplacementDir));
        }

        private static void GetImage(AsyncGPUReadbackRequest request, Texture texture, int frame, DirectoryInfo folder)
        {
            NativeArray<byte> rawImage = request.GetData<byte>(0);
            NativeArray<byte> nativePng = ImageConversion.EncodeNativeArrayToPNG(rawImage, texture.graphicsFormat,
                (uint)texture.width, (uint)texture.height, 0);
            byte[] asManaged = nativePng.ToArray();
            nativePng.Dispose();

            string fullFilePath = Path.Combine(folder.FullName, $"{frame}.png");
            File.WriteAllBytes(fullFilePath, asManaged);
        }

        /*void OnPreRender(){
            MainCamera.targetTexture = MainTexture;
        }
     
        void OnPostRender() {
            MainCamera.targetTexture = null;
            Graphics.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), MainTexture);
        }*/
    }
}
