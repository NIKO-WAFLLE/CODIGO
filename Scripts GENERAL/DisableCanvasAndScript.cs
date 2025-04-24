using UnityEngine;
using UnityEngine.UI;

public class DisableCanvasAndScript : MonoBehaviour
{
    // Public references for the button, script, and canvas.
    public Button button;
    public MonoBehaviour script;
    public Canvas canvas;

    void Start()
    {
        // Make sure the button has an event listener when clicked.
        if (button != null)
        {
            button.onClick.AddListener(DisableElements);
        }
    }

    // Function to disable the script and the canvas.
    void DisableElements()
    {
        if (script != null)
        {
            script.enabled = false;  // Disable the script.
        }

        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);  // Disable the canvas.
        }
    }
}
