using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    private Rigidbody bulletRb;
    public float bulletPower = 0f;
    public float lifeTime = 4f;

    private float time = 0f;
    
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();

        bulletRb.velocity = this.transform.forward*bulletPower;

    }

    
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
