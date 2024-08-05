using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamara : MonoBehaviour
{

    void Update()
    {
        transform.forward=Camera.main.transform.forward;
    }
}
