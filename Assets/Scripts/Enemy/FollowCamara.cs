using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamara : MonoBehaviour
{
    void Update()
    {
        
        if (Camera.main != null)
        {
          
            transform.forward = Camera.main.transform.forward;
        }
    }
}

