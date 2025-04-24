using UnityEngine;
using System.Collections.Generic;

public class CurvedRayMerger : MonoBehaviour
{
    // Listas de objetos de inicio y fin para cada par de rayos
    public List<Transform> startObjects;
    public List<Transform> endObjects;

    // Parámetros configurables del rayo
    public int curveResolution = 20; // Número de puntos que forman la curva
    public Color rayColor = Color.red; // Color del rayo
    public float curveIntensity = 2.0f; // Intensidad de la curvatura
    public float rayWidth = 0.1f; // Grosor del rayo

    // Lista de LineRenderers, uno para cada par de objetos
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

    // Método llamado al inicio del juego
    void Start()
    {
        // Inicializa un LineRenderer por cada par de objetos de inicio y fin
        for (int i = 0; i < startObjects.Count && i < endObjects.Count; i++)
        {
            GameObject lineObject = new GameObject("CurvedRay_" + i);
            lineObject.transform.SetParent(transform);

            // Se crea un LineRenderer para cada par de objetos
            LineRenderer lr = lineObject.AddComponent<LineRenderer>();
            lineRenderers.Add(lr);

            // Configura el LineRenderer para este par de objetos
            lr.positionCount = curveResolution;
            lr.startColor = rayColor;
            lr.endColor = rayColor;
            lr.startWidth = rayWidth;
            lr.endWidth = rayWidth;
            lr.material = new Material(Shader.Find("Sprites/Default"));
        }
    }

    // Método llamado cada cuadro para actualizar todos los rayos
    void Update()
    {
        // Recorre cada par de objetos de inicio y fin y actualiza el rayo correspondiente
        for (int i = 0; i < Mathf.Min(startObjects.Count, endObjects.Count); i++)
        {
            UpdateRay(startObjects[i], endObjects[i], lineRenderers[i]);
        }
    }

    // Actualiza la posición y forma del rayo según los objetos de inicio y fin
    void UpdateRay(Transform startObject, Transform endObject, LineRenderer lineRenderer)
    {
        // Si alguno de los objetos de inicio o fin no está asignado, no hacemos nada
        if (startObject == null || endObject == null) return;

        // Obtiene las posiciones centrales de los objetos de inicio y fin
        Vector3 start = GetObjectCenter(startObject);
        Vector3 end = GetObjectCenter(endObject);

        // Lista para almacenar las posiciones de la curva
        List<Vector3> positions = new List<Vector3>();

        // Genera los puntos de la curva
        for (int i = 0; i < curveResolution; i++)
        {
            // Calcula el valor 't' para interpolar entre los puntos de inicio y fin
            float t = i / (float)(curveResolution - 1);

            // Aplica una fórmula de parábola para la curvatura
            float parabolaT = Mathf.Sin(t * Mathf.PI) * curveIntensity;

            // Interpola entre los puntos y agrega la curvatura hacia arriba
            Vector3 point = Vector3.Lerp(start, end, t) + Vector3.up * parabolaT;

            // Agrega el punto a la lista de posiciones
            positions.Add(point);
        }

        // Establece las posiciones del LineRenderer para dibujar la curva
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());

        // Vuelve a asignar el color y grosor
        lineRenderer.startColor = rayColor;
        lineRenderer.endColor = rayColor;
        lineRenderer.startWidth = rayWidth;
        lineRenderer.endWidth = rayWidth;
    }

    // Obtiene el centro del objeto. Si tiene un Renderer, usa su centro; si no, usa la posición del objeto
    Vector3 GetObjectCenter(Transform obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        return renderer != null ? renderer.bounds.center : obj.position;
    }
}
