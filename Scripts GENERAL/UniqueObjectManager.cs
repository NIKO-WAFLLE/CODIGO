using UnityEngine;

public class UniqueObjectManager : MonoBehaviour
{
    public int objectNumber; // Número identificador único del objeto
    public GameObject objectPrefab; // Prefab del objeto asignado desde el Inspector
    private static int existingObjectNumber = -1;
    private static UniqueObjectManager existingInstance;

    void Awake()
    {
        UniqueObjectManager[] existingObjects = FindObjectsOfType<UniqueObjectManager>();
        foreach (var obj in existingObjects)
        {
            if (obj != this && obj.objectNumber == objectNumber)
            {
                Debug.Log("Un objeto con este número ya existe en la escena. Eliminando el duplicado.");
                Destroy(gameObject);
                return;
            }
        }
        
        existingInstance = this;
        existingObjectNumber = objectNumber;
    }

    public void TrySpawnObject(Vector3 spawnPoint)
    {
        if (existingInstance != null && existingObjectNumber == objectNumber)
        {
            Debug.Log("El objeto ya existe en la escena. No se generará otro.");
            return;
        }
        
        existingInstance = Instantiate(objectPrefab, spawnPoint, Quaternion.identity).GetComponent<UniqueObjectManager>();
        existingObjectNumber = objectNumber;
        Debug.Log("Nuevo objeto generado en la escena.");
    }
}