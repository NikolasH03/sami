using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dinero; 
    private int contador = 0;
    
    void Start()
    {  
        dinero.text = contador.ToString();
    }

    void Update()
    {         
            dinero.text = contador.ToString();    
    }

    public int getContador()
    {
        return contador;
    }
    public void setContador(int cont)
    {
        contador += cont;
    }
}

