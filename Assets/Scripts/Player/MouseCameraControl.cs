using UnityEngine;

public class MouseCameraControl : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Referencia al cuerpo del jugador o al objeto a rotar.

    private float xRotation = 0f;

    void Start()
    {
        // Oculta el cursor y lo bloquea en el centro de la pantalla.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Obtener los movimientos del ratón.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotar en el eje X (vertical).
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotación vertical.

        // Aplicar la rotación vertical.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotar el cuerpo del jugador (horizontal).
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

