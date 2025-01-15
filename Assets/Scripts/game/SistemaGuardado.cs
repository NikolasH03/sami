using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SistemaGuardado
{
    public static void GuardarDatos (Inventario inventario, HealthBar healthbar)
    {
        BinaryFormatter formatter = new BinaryFormatter (); 
        string path = Application.persistentDataPath + "/jugador.fuckyou";
        FileStream stream = new FileStream(path, FileMode.Create);

        DatosGuardado datos = new DatosGuardado(inventario, healthbar);

        formatter.Serialize(stream, datos);
        stream.Close();
    }
    public static DatosGuardado CargarDatos()
    {
        string path = Application.persistentDataPath + "/jugador.fuckyou";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter ();
            FileStream stream = new FileStream(path, FileMode.Open);

            DatosGuardado datos = formatter.Deserialize(stream) as DatosGuardado;
            stream.Close();

            return datos;
        }
        else
        {
            Debug.LogError("No se encontró el archivo de guardado en " + path);
            return null;
        }
    }
}
