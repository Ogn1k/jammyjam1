using UnityEngine;
using UnityEngine.UI;

public class TransparentButton : MonoBehaviour
{
    public float threshold;
    public bool allowClick;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        //threshold = image.alphaHitTestMinimumThreshold;
    }
    void Update()
    {
        image.raycastTarget = allowClick;
        image.alphaHitTestMinimumThreshold = threshold;
    }
}
