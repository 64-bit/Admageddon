using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Admageddon
{
    [RequireComponent(typeof(Camera))]
    public class ReplacementCamera : MonoBehaviour
    {
        public Shader SegmentationShader;

        void Awake()
        {
            var camera = GetComponent<Camera>();
            camera.SetReplacementShader(SegmentationShader, null);
        }
    }
}