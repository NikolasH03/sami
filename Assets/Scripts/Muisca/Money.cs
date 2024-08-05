using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public TextMeshProUGUI dinero; // Referencia al objeto de texto que quieres actualizar
    public int contador = 0;
    
    void Start()
    {

        // Actualizar el texto inicialmente al valor del contador
        dinero.text = contador.ToString();
    }

    void Update()
    {
        
 
            // Actualizar el texto con el nuevo valor del contador
            dinero.text = contador.ToString();
        
    }
}

