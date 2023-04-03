using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
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

        public string DataDirectory = "C:\\Data\\ML\\AdData";

        private DirectoryInfo BaseDir;

        private DirectoryInfo MainDir;
        private DirectoryInfo ReplacementDir;

        /// <summary>
        /// This event will be invoked a frame before the frame is captured
        /// Useful for setting up the scene & segmentations before capture
        /// </summary>
        public event Action PreGrabFrame;

        private bool _isGrabbing = false;

        // Start is called before the first frame update
        void Awake()
        {
            VisualTexture = new RenderTexture(Width, Height, 32);
            SegmentationTexture = new RenderTexture(Width, Height, 32);

            BaseDir = new DirectoryInfo(GetDataDirectoryForDateTimeNow());
            if (BaseDir.Exists == false)
            {
                BaseDir.Create();
            }
            
            MainDir = BaseDir.CreateSubdirectory("Main");
            ReplacementDir = BaseDir.CreateSubdirectory("Replacement");

            VisualCamera.targetTexture = VisualTexture;
            SegmentationCamera.targetTexture = SegmentationTexture;
        }

        private string GetDataDirectoryForDateTimeNow()
        {
            return Path.Combine(DataDirectory, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        }

        public void StartCapture(int frames, float timestep)
        {
            if (_isGrabbing)
            {
                Debug.LogError("Already capturing");
                return;
            }

            _isGrabbing = true;
            StartCoroutine(CaptureProcess(frames, timestep));
        }

        IEnumerator CaptureProcess(int count, float timeStep)
        {
            for (int i = 0; i < count; i++)
            {
                PreGrabFrame?.Invoke();
                yield return null;
                GetImagesForFrame(i);
                Debug.Log($"Frames remaining:{count - i}");
                yield return new WaitForSeconds(timeStep);
            }

            _isGrabbing = false;
            Debug.Log($"Finished capturing {count} frames");
        }

        private void GetImagesForFrame(int frame)
        {
            AsyncGPUReadback.Request(VisualTexture, 0, request => GetImage(request, VisualTexture, frame, MainDir));
            AsyncGPUReadback.Request(SegmentationTexture, 0, request => GetImage(request, SegmentationTexture, frame, ReplacementDir));
        }

        private static void GetImage(AsyncGPUReadbackRequest request, Texture texture, int frame, DirectoryInfo folder)
        {
            NativeArray<byte> rawImage = request.GetData<byte>(0);
            
            //Get graphics format width and height on the main thread
            uint width = (uint)texture.width;
            uint height = (uint)texture.height;
            GraphicsFormat graphicsFormat = texture.graphicsFormat;

            Task.Run(() =>
            {
                try
                {
                    NativeArray<byte> nativePng = ImageConversion.EncodeNativeArrayToPNG(rawImage, graphicsFormat, width, height, 0);
                    byte[] asManaged = nativePng.ToArray();
                    nativePng.Dispose();

                    string fullFilePath = Path.Combine(folder.FullName, $"{frame}.png");
                    File.WriteAllBytes(fullFilePath, asManaged);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }

            });
        }
    }
}