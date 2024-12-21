using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManagement : MonoBehaviour
{

    public static ResourceManagement instance;

    [SerializeField] TextMeshProUGUI dineroUI;
    private int dinero = 0;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        dineroUI.text = dinero.ToString();
    }

    void Update()
    {
        dineroUI.text = dinero.ToString();
    }

    public int getContador()
    {
        return dinero;
    }
    public void setContador(int cont)
    {
        dinero += cont;
    }

    public void enemigoMuerto(int tipoEnemigo)
    {
        switch (tipoEnemigo)
        {
            case 1: dinero += 50; break;
            case 2: dinero += 200; break;
            default: Debug.Log("No existe ese tipo de enemigo"); break;
        }
    }
}
