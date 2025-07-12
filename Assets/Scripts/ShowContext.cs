using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowContext : MonoBehaviour
{
    public GameObject tooltipPanel;
    public float maxDistance = 10f;
    public LayerMask interactLayers;
    private GameObject lastHoverableObject;
    private float fadeSpeed = 2f;
    private CanvasGroup panelCanvasGroup;
    private bool isHovering;
    public ObjectKeeper objectKeeper;

    void Start()
    {
        panelCanvasGroup = tooltipPanel.GetComponent<CanvasGroup>();
        panelCanvasGroup.alpha = 0;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        float targetAlpha = isHovering ? 1f : 0f;
        float fadeSpeedT = isHovering ? fadeSpeed : 8;
        panelCanvasGroup.alpha = Mathf.Lerp(panelCanvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeedT);

        if (Physics.Raycast(ray, out hit, maxDistance, interactLayers))
        {
            if (hit.collider.gameObject != lastHoverableObject)
            {

                //tooltipPanel.SetActive(true);
                lastHoverableObject = hit.collider.gameObject;
                isHovering = true;

            }
            if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < objectKeeper.objStatePanels.Count; i++)
                    {
                        if (objectKeeper.objStatePanels[i].GetComponent<ObjectChanger>().currentCustomObject.curObject == hit.collider.gameObject)
                        {
                            objectKeeper.objStatePanels[i].GetComponent<ObjectChanger>().ChangeState();
                        }
                    }
                }
            }
        else
        {
            //tooltipPanel.SetActive(false);
            lastHoverableObject = null;
            isHovering = false;

        }
        
    }

}
