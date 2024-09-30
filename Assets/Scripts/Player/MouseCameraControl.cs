using UnityEngine;

public class ThirdPersonAimCamera : MonoBehaviour
{
    public Transform target; // El jugador o personaje al que seguirá la cámara
    public float mouseSensitivity = 100f; // Sensibilidad del ratón
    public Vector3 cameraOffset = new Vector3(-1f, 1.5f, 2f); // Posición fija sobre el hombro

    private float xRotation = 0f; // Control de la rotación vertical

    void LateUpdate()
    {
        if (target == null) return;

        // Obtener los movimientos del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotación vertical (cámara)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40f, 60f); // Limita la rotación vertical

        // Rotación horizontal (jugador)
        target.Rotate(Vector3.up * mouseX); // Gira el personaje en el eje Y

        // Mantén la cámara en la posición fija respecto al personaje (sobre el hombro)
        transform.position = target.position + target.TransformDirection(cameraOffset);

        // Aplica la rotación vertical a la cámara (solo sobre el eje X)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Apunta la cámara en la misma dirección que el personaje (solo para el eje Y)
        transform.LookAt(target.position + Vector3.up * 1.5f); // Asegúrate de mirar un poco por encima del personaje
    }
}





