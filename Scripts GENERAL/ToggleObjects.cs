using UnityEngine;
using UnityEngine.UI;

public class ToggleObjects : MonoBehaviour
{
    public GameObject objeto1;
    public GameObject objeto2;
    public Button boton;

    private bool activos = true;

    void Start()
    {
        if (boton != null)
        {
            boton.onClick.AddListener(ToggleEstado);
        }
    }

    void ToggleEstado()
    {
        activos = !activos;
        
        if (objeto1 != null) objeto1.SetActive(activos);
        if (objeto2 != null) objeto2.SetActive(activos);
    }
}
