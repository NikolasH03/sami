using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public TextMeshProUGUI dinero; 
    public int contador = 0;
    
    void Start()
    {  
        dinero.text = contador.ToString();
    }

    void Update()
    {         
            dinero.text = contador.ToString();    
    }
}

