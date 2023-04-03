using System.Collections;
using System.Collections.Generic;
using Admageddon;
using UnityEngine;

public class DropoutTraget : MonoBehaviour , IDropout
{
    public void SetIsEnabled(bool isEnabled)
    {
        gameObject.SetActive(isEnabled);
    }
}
