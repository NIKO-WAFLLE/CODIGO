using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject panelA;
    public GameObject panelB;

    public void TogglePanels()
    {
        if (panelA != null && panelB != null)
        {
            bool isAActive = panelA.activeSelf;

            panelA.SetActive(!isAActive);
            panelB.SetActive(isAActive);
        }
    }
}
