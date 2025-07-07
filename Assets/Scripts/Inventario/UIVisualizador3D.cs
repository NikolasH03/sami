using UnityEngine;
using TMPro;
public class UIVisualizador3D : MonoBehaviour
{
    public static UIVisualizador3D instance;
    [SerializeField] TextMeshProUGUI textoNombre;
    [SerializeField] TextMeshProUGUI textoDescripcion;
    [SerializeField] Transform contenedorModelo3D;
    [SerializeField] GameObject contenedor;

    public float velocidadRotacion = 50f;


    private GameObject instanciaActual;

    private void Awake()
    {
        instance = this;

        contenedor.SetActive(false);
    }

    private void Update()
    {
        float rotX = 0f;
        float rotY = 0f;

        if (Input.GetKey(KeyCode.A)) rotY = -1f;
        if (Input.GetKey(KeyCode.D)) rotY = 1f;
        if (Input.GetKey(KeyCode.W)) rotX = -1f;
        if (Input.GetKey(KeyCode.S)) rotX = 1f;

        Vector3 rotacion = new Vector3(rotX, rotY, 0f) * velocidadRotacion * Time.unscaledDeltaTime;
        contenedorModelo3D.Rotate(rotacion, Space.Self);
    }

    public void Mostrar(ColeccionableData data)
    {
        if (instanciaActual != null)
            Destroy(instanciaActual);

        instanciaActual = Instantiate(data.modelo3D, contenedorModelo3D);
        instanciaActual.transform.localPosition = Vector3.zero;
        instanciaActual.transform.localRotation = Quaternion.identity;
        instanciaActual.transform.localScale *= 10;

        instanciaActual.layer = LayerMask.NameToLayer("Coleccionable");
        foreach (Transform t in instanciaActual.GetComponentsInChildren<Transform>())
            t.gameObject.layer = LayerMask.NameToLayer("Coleccionable");

        textoNombre.text = data.nombre;
        textoDescripcion.text = data.descripcion;

        contenedor.SetActive(true);
    }
    private void AjustarEscalaModelo(GameObject modelo, float tamanoDeseado = 1.5f)
    {
        float mayorDimension = 0f;

        MeshFilter[] filtros = modelo.GetComponentsInChildren<MeshFilter>();
        foreach (var filtro in filtros)
        {
            Vector3 size = filtro.sharedMesh.bounds.size;
            float magnitud = size.magnitude * filtro.transform.lossyScale.magnitude;

            if (magnitud > mayorDimension)
                mayorDimension = magnitud;
        }

        if (mayorDimension == 0) return;

        float factorEscala = tamanoDeseado / mayorDimension;

        modelo.transform.localScale *= factorEscala;
    }

    public void Cerrar()
    {
        if (instanciaActual != null)
            Destroy(instanciaActual);
        contenedor.SetActive(false);
    }
}
