using UnityEngine;

public class CrosshairFollowCamera : MonoBehaviour
{
    public Transform cameraTransform; // La cámara que queremos seguir.

    void Update()
    {
        // Hacer que el canvas siga la posición y rotación de la cámara.
        transform.position = cameraTransform.position + cameraTransform.forward * 2f; // Ajusta la distancia según necesites.
        transform.rotation = cameraTransform.rotation;
    }
}

