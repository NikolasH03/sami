using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    public Transform shootSpawn;
    public bool shooting = false;
    public GameObject bulletPrefab;
    public GameObject crosshair;
    public GameObject mainCamera;
    public GameObject aimCamera;

    [SerializeField] Player player;
    void Start()
    {
        crosshair.SetActive(false);
        aimCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(shootSpawn.position, shootSpawn.forward * 10f, Color.red);
        Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.forward * 10f, Color.blue);

        RaycastHit cameraHit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out cameraHit))
        {
            Vector3 shootDirection = cameraHit.point - shootSpawn.position;
            shootSpawn.rotation = Quaternion.LookRotation(shootDirection);

            if (Input.GetMouseButtonDown(1))
            {
                player.anim.SetBool("aiming",true);
                crosshair.SetActive(true);
                aimCamera.SetActive(true);


            }
            if (Input.GetMouseButtonUp(1))
            {
                player.anim.SetBool("aiming", false);
                crosshair.SetActive(false);
                aimCamera.SetActive(false);
            }
            if (player.anim.GetBool("aiming") && Input.GetMouseButtonDown(0))
            {
                Shoot();
            }

        }
    }
public void Shoot()
    {
        Instantiate(bulletPrefab,shootSpawn.position, shootSpawn.rotation);
    }
}

