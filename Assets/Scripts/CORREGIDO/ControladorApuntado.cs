using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class ControladorApuntado : MonoBehaviour
{
   [SerializeField] private CinemachineVirtualCamera camaraApuntado;
    [SerializeField] private float sensibilidadNormal;
    [SerializeField] private float sensibilidadApuntado;
    [SerializeField] private LayerMask apuntadoColliderLayerMask = new LayerMask();
    [SerializeField] private Transform prefabProyectil;
    [SerializeField] private Transform spawnPosicionProyectil;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private bool terminoAnimacionDisparo;

    //referencias a otros codigos
    [SerializeField] ControladorCambioArmas controladorCambioArmas;
    [SerializeField] ControladorMovimiento controladorMovimiento;

    public void Start()
    {
        crosshair.SetActive(false);
        terminoAnimacionDisparo = true;
    }
    private void Update()
    {
        controladorCambioArmas = GetComponent<ControladorCambioArmas>();
        controladorMovimiento = GetComponent<ControladorMovimiento>();
        animator = GetComponent<Animator>();
        int numeroArma = controladorCambioArmas.getterArma();

        Vector2 centroPantalla = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(centroPantalla);
        Vector3 posicionMouse = Vector3.zero;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, apuntadoColliderLayerMask))
        {
            posicionMouse = raycastHit.point;
        }
        else
        {
            centroPantalla = ray.GetPoint(10);
            posicionMouse = ray.GetPoint(10);
        }

        if (InputJugador.instance.apuntar && numeroArma==2)
        {

            estaApuntando(posicionMouse);

            if (InputJugador.instance.disparar)
            {
                if (terminoAnimacionDisparo)
                {
                    disparar(posicionMouse);
                }

            }

        }
        else
        {
            noEstaApuntando();
        }

    }

    public void estaApuntando(Vector3 posicionMouse)
    {
        camaraApuntado.gameObject.SetActive(true);
        crosshair.SetActive(true);
        controladorMovimiento.SetSensibilidad(sensibilidadApuntado);
        controladorMovimiento.SetRotacionAlMoverse(false);
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
        Vector3 TargetApuntado = posicionMouse;
        TargetApuntado.y = transform.position.y;
        Vector3 direccionApuntado = (TargetApuntado - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, direccionApuntado, Time.deltaTime * 20f);
    }

    public void noEstaApuntando()
    {
        camaraApuntado.gameObject.SetActive(false);
        controladorMovimiento.SetSensibilidad(sensibilidadNormal);
        controladorMovimiento.SetRotacionAlMoverse(true);
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        crosshair.SetActive(false);
    }

    public void disparar(Vector3 posicionMouse)
    {
        Vector3 aimDir = (posicionMouse - spawnPosicionProyectil.position).normalized;

        animator.SetBool("Disparo",true);
        Instantiate(prefabProyectil, spawnPosicionProyectil.position, Quaternion.LookRotation(aimDir, Vector3.up)); 
        terminoAnimacionDisparo = false;


    }
    public void recargar()
    {
        animator.SetBool("Disparo", false);
        terminoAnimacionDisparo = true;
        InputJugador.instance.disparar = false;
    }
}
