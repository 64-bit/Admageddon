using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Admageddon
{
    //Script to rotate the object smoothly over time
    public class Rotate : MonoBehaviour
    {
        public float speed = 15.0f;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.RotateAround(Vector3.up, speed * Time.deltaTime);
        }
    }

}
