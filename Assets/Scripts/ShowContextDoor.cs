using UnityEngine;
using UnityEngine.SceneManagement;
public class ShowContextDoor : MonoBehaviour
{
    public GameObject tooltipPanel;
    public float maxDistance = 10f;
    public LayerMask interactLayers;
    private GameObject lastHoverableObject;
    private float fadeSpeed = 2f;
    private CanvasGroup panelCanvasGroup;
    private bool isHovering;
    public int scene;
    public LvlLogic lvlLogic;

    void Start()
    {
        lvlLogic = GameObject.Find("LvlLogic").GetComponent<LvlLogic>();
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
                    lvlLogic.SetResult();
                    SceneManager.LoadScene(scene);
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
