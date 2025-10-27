using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public class VFXPool : MonoBehaviour
{
    private Dictionary<VFXData, Queue<GameObject>> pools = new Dictionary<VFXData, Queue<GameObject>>();

    public static VFXPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayVFX(VFXData data, Vector3 position, Quaternion rotation)
    {
        if (!pools.ContainsKey(data))
        {
            pools[data] = new Queue<GameObject>();
        }

        GameObject obj;
        if (pools[data].Count > 0)
        {
            obj = pools[data].Dequeue();

            // Si el objeto fue destruido por cambio de escena, elimínalo y crea uno nuevo
            if (obj == null)
            {
                obj = Instantiate(data.prefab, transform);
            }
            else
            {
                obj.SetActive(true);
            }
        }
        else
        {
            obj = Instantiate(data.prefab);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        StartCoroutine(DespawnVFX(data, obj, data.lifetime));
    }

    private IEnumerator<WaitForSeconds> DespawnVFX(VFXData data, GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        if (obj != null)
        {
            obj.SetActive(false);

            // Si el diccionario se reseteó (por seguridad en cambios de escena)
            if (!pools.ContainsKey(data))
                pools[data] = new Queue<GameObject>();

            pools[data].Enqueue(obj);
        }

    }
}
