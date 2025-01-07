using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;  

public class ControladorCombateDistancia : MonoBehaviour
{
    public Transform shootSpawn;
    public GameObject bulletPrefab;
    public GameObject crosshair;
    public CinemachineFreeLook freelook;  // Cinemachine FreeLook Camera
    public CinemachineVirtualCamera aimCamera; // Cinemachine Virtual Camera para apuntado
    public bool shooting;

    public Transform followTarget; // El objeto que la cámara sigue (Follow)
    public float mouseSensitivity = 100f; // Sensibilidad del ratón
    public float maxPitch = 80f; // Límite superior de rotación en X
    public float minPitch = -80f; // Límite inferior de rotación en X

    private float yaw = 0f;  // Rotación en Y (horizontal)
    private float pitch = 0f;  // Rotación en X (vertical)

    [SerializeField] ControladorMovimiento controladorMovimiento;

    private int freelookPriority = 10;
    private int aimPriority = 10;

    void Start()
    {
        crosshair.SetActive(false);
        aimCamera.Priority = freelookPriority - 1;
        shooting = false;


        if (aimCamera.Follow != null)
        {
            followTarget = aimCamera.Follow;
        }
        else
        {
            Debug.LogError("No se ha asignado un Follow a la cámara virtual.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Botón derecho del ratón
        {
            EnableAimingMode(true);
        }

        if (Input.GetMouseButtonUp(1)) // Soltar el botón derecho del ratón
        {
            EnableAimingMode(false);
        }

        if (controladorMovimiento.anim.GetBool("aiming"))
        {
            AimAndShoot();
            HandleMouseLook(); // Llamamos a la función de manejo de mouse
        }
    }

    void EnableAimingMode(bool isAiming)
    {
        controladorMovimiento.anim.SetBool("aiming", isAiming);
        crosshair.SetActive(isAiming);

        if (isAiming)
        {
            aimCamera.Priority = aimPriority;  // Aumenta la prioridad de la cámara de apuntado
            freelook.Priority = freelookPriority - 1; // Reduce la prioridad de la cámara FreeLook
        }
        else
        {
            freelook.Priority = freelookPriority + 1; // Aumenta de nuevo la prioridad de la cámara FreeLook
            aimCamera.Priority = aimPriority - 1; // Reduce la prioridad de la cámara de apuntado
        }

        // Bloquea o desbloquea el movimiento del jugador
        controladorMovimiento.canMove = !isAiming;

        // Si se desactiva el apuntado, también debemos restablecer la rotación del followTarget
        //if (!isAiming)
        //{
        //    followTarget.localRotation = Quaternion.identity;  // Reinicia la rotación del followTarget
        //}
    }

    void AimAndShoot()
    {
        RaycastHit cameraHit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out cameraHit))
        {
            Vector3 shootDirection = cameraHit.point - shootSpawn.position;
            shootSpawn.rotation = Quaternion.LookRotation(shootDirection);

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (!shooting)
        {
            shooting=true;
            controladorMovimiento.anim.Play("fire");
            Instantiate(bulletPrefab, shootSpawn.position, shootSpawn.rotation);
        }
        else
        {
            return;
        }
        
        
    }

    // Controla la rotación del Follow target con el movimiento del mouse
    void HandleMouseLook()
    {
        // Capturamos el movimiento del mouse en X (yaw) e Y (pitch)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Actualizamos el yaw y pitch
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch); // Limita la rotación en X

        // Aplicamos la rotación al objeto Follow en base a los valores de yaw y pitch
        followTarget.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
    
}




