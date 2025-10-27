using DG.Tweening;
using UnityEngine;

public class AutoTargeting : MonoBehaviour
{
    [Header("Parámetros de búsqueda")]
    [SerializeField] private float rangoBusqueda = 6f;
    [SerializeField] private float anguloBusqueda = 120f;
    [SerializeField] private LayerMask capaEnemigos;
    [SerializeField] private Transform referenciaRotacion;
    [SerializeField] private float velocidadGiro = 10f;

    private Transform enemigoObjetivo;

    public Transform EnemigoObjetivo => enemigoObjetivo;

    public void BuscarSegunDireccionDeMirada(Vector2 inputMirar)
    {
        // Si no hay input, no hacemos nada especial
        if (inputMirar.sqrMagnitude < 0.1f)
            return;

        // Convertir input de cámara a dirección en el mundo
        Camera cam = Camera.main;
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 direccionMirada = (forward * inputMirar.y + right * inputMirar.x).normalized;

        // Buscar enemigos en el rango
        Collider[] enemigos = Physics.OverlapSphere(transform.position, rangoBusqueda, capaEnemigos);

        float mejorPuntaje = Mathf.Cos(anguloBusqueda * 0.5f * Mathf.Deg2Rad); // Cos para comparación directa
        Transform mejorObjetivo = null;

        foreach (Collider col in enemigos)
        {
            Vector3 dirEnemigo = (col.transform.position - transform.position).normalized;
            dirEnemigo.y = 0;
            float dot = Vector3.Dot(direccionMirada, dirEnemigo);

            if (dot > mejorPuntaje) // Más cercano al centro de la dirección de mirada
            {
                mejorPuntaje = dot;
                mejorObjetivo = col.transform;
            }
        }

        // Si encontramos objetivo, orientamos el personaje hacia él
        if (mejorObjetivo != null)
        {
            enemigoObjetivo = mejorObjetivo;
            Vector3 dir = (enemigoObjetivo.position - transform.position).normalized;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);
            referenciaRotacion.DORotateQuaternion(rot, 0.15f); // suave y rápido
        }
    }
}
