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

    [Header("Desplazamiento hacia el objetivo")]
    [SerializeField] private float distanciaMinimaAtaque = 1.6f;
    [SerializeField] private float duracionAproximacion = 0.20f;
    [SerializeField] private Ease easingAproximacion = Ease.OutQuad;

    [Header("Desplazamiento sin objetivo")]
    [SerializeField] private float distanciaDesplazamientoLibre = 2f; // Distancia cuando no hay enemigo
    [SerializeField] private float duracionDesplazamientoLibre = 0.25f;

    [Header("Dash")]
    [SerializeField] private float distanciaDash = 4f;
    [SerializeField] private float duracionDash = 0.15f;
    [SerializeField] private Ease easingDash = Ease.OutQuint;

    private Transform enemigoObjetivo;
    private Tween movimientoTween;

    public Transform EnemigoObjetivo => enemigoObjetivo;

    public void BuscarSegunDireccionDeMirada(Vector2 inputMirar)
    {
        if (inputMirar.sqrMagnitude < 0.1f)
            return;

        // Convertir input a dirección del mundo
        Camera cam = Camera.main;
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 direccionMirada = (forward * inputMirar.y + right * inputMirar.x).normalized;

        // Buscar enemigos
        Collider[] enemigos = Physics.OverlapSphere(transform.position, rangoBusqueda, capaEnemigos);
        float mejorPuntaje = Mathf.Cos(anguloBusqueda * 0.5f * Mathf.Deg2Rad);
        Transform mejorObjetivo = null;

        foreach (Collider col in enemigos)
        {
            Vector3 dirEnemigo = (col.transform.position - transform.position).normalized;
            dirEnemigo.y = 0;

            float dot = Vector3.Dot(direccionMirada, dirEnemigo);

            if (dot > mejorPuntaje)
            {
                mejorPuntaje = dot;
                mejorObjetivo = col.transform;
            }
        }

        if (mejorObjetivo != null)
        {
            enemigoObjetivo = mejorObjetivo;

            // ROTACIÓN HACIA EL ENEMIGO
            Vector3 dir = (enemigoObjetivo.position - transform.position).normalized;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);
            referenciaRotacion.DORotateQuaternion(rot, 0.15f);

            // MOVIMIENTO HACIA EL ENEMIGO
            MoverHaciaObjetivoSiEsNecesario();
        }
        else
        {
            // NO HAY ENEMIGO CERCA: Desplazarse un poco en la dirección de mirada
            DesplazarseEnDireccion(direccionMirada, distanciaDesplazamientoLibre, duracionDesplazamientoLibre, easingAproximacion);

            // Rotar hacia donde mira
            Quaternion rot = Quaternion.LookRotation(direccionMirada);
            referenciaRotacion.DORotateQuaternion(rot, 0.15f);
        }
    }

    private void MoverHaciaObjetivoSiEsNecesario()
    {
        if (enemigoObjetivo == null)
            return;

        float distanciaActual = Vector3.Distance(transform.position, enemigoObjetivo.position);

        // Si ya estoy cerca, no me muevo
        if (distanciaActual <= distanciaMinimaAtaque + 0.1f)
            return;

        // Punto final a la distancia exacta deseada
        Vector3 direccion = (enemigoObjetivo.position - transform.position).normalized;
        Vector3 posicionObjetivo = enemigoObjetivo.position - direccion * distanciaMinimaAtaque;

        // Cancelar movimiento anterior si lo hay
        movimientoTween?.Kill();

        // Deslizarse hacia la posición calculada
        movimientoTween = transform.DOMove(posicionObjetivo, duracionAproximacion)
                                   .SetEase(easingAproximacion)
                                   .SetUpdate(UpdateType.Fixed);
    }

    /// <summary>
    /// Realiza un dash hacia atrás (independiente de la dirección del input)
    /// </summary>
    public void EjecutarDash()
    {
        // Obtener la dirección hacia atrás basada en la rotación actual del jugador
        Vector3 direccionAtras = -referenciaRotacion.forward;
        direccionAtras.y = 0;
        direccionAtras.Normalize();

        // Ejecutar el dash hacia atrás
        DesplazarseEnDireccion(direccionAtras, distanciaDash, duracionDash, easingDash);

        // NO rotamos al jugador durante el dash, mantiene su orientación actual
    }

    /// <summary>
    /// Función auxiliar para desplazarse en una dirección específica
    /// </summary>
    private void DesplazarseEnDireccion(Vector3 direccion, float distancia, float duracion, Ease easing)
    {
        direccion.y = 0;
        direccion.Normalize();

        Vector3 posicionObjetivo = transform.position + direccion * distancia;

        // Cancelar movimiento anterior
        movimientoTween?.Kill();

        // Realizar el desplazamiento
        movimientoTween = transform.DOMove(posicionObjetivo, duracion)
                                   .SetEase(easing)
                                   .SetUpdate(UpdateType.Fixed);
    }

    // Función opcional para cancelar cualquier movimiento en curso
    public void CancelarMovimiento()
    {
        movimientoTween?.Kill();
    }

    private void OnDestroy()
    {
        movimientoTween?.Kill();
    }

    // Visualización en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoBusqueda);

        if (enemigoObjetivo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, enemigoObjetivo.position);
        }
    }
}

