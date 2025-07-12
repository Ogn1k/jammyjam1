using UnityEngine;
using UnityEngine.UI;

public class MultiArrayProgressBar : MonoBehaviour
{
    public RectTransform segment1;
    public RectTransform segment2;
    public RectTransform segment3;

    public float value1;
    public float value2;
    public float value3;
    private float totalWidth;

    void Start()
    {
        totalWidth = ((RectTransform)transform).rect.width;
        UpdateBar();
    }

    private void OnValidate()
    {
        //totalWidth = ((RectTransform)transform).rect.width;
        UpdateBar();
    }

    public void SetValue1(float value, float animationSpeed)
    {
        value1 =  Mathf.Lerp(value1, value, Time.deltaTime * animationSpeed);;
        UpdateBar();
    }
    public void SetValue2(float value, float animationSpeed)
    {
        value2 =  Mathf.Lerp(value2, value, Time.deltaTime * animationSpeed);;
        UpdateBar();
    }

    public void SetValue3(float value, float animationSpeed)
    {
        value3 =  Mathf.Lerp(value3, value, Time.deltaTime * animationSpeed);;
        UpdateBar();
    }

    public void UpdateBar()
    {
        float total = value1 + value2 + value3;
        if (total <= 0f) total = 1f;

        float percent1 = value1 / total;
        float percent2 = value2 / total;
        float percent3 = value3 / total;

        float width1 = totalWidth * percent1;
        float width2 = totalWidth * percent2;
        float width3 = totalWidth * percent3;

        if (segment1)
        {
            segment1.sizeDelta = new Vector2(width1, segment1.sizeDelta.y);
            segment1.anchoredPosition = new Vector2(0, 0);
        }

        if (segment2)
        {
            segment2.sizeDelta = new Vector2(width2, segment2.sizeDelta.y);
            segment2.anchoredPosition = new Vector2(width1, 0);
        }

        if (segment3)
        {
            segment3.sizeDelta = new Vector2(width3, segment3.sizeDelta.y);
            segment3.anchoredPosition = new Vector2(width1 + width2, 0);
        }
    }
}
