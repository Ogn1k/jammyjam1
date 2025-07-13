using System.Collections.Specialized;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class LvlLogic : MonoBehaviour
{
    public ObservableCollection<GameObject> cyberlist = new ObservableCollection<GameObject>();
    public ObservableCollection<GameObject> fantasylist = new ObservableCollection<GameObject>();
    public ObservableCollection<GameObject> normallist = new ObservableCollection<GameObject>();
    public MultiArrayProgressBar progressBar;
    public StoryResultObj result;
    float animationSpeed = 5f;
    void Start()
    {
        cyberlist.CollectionChanged += (sender, e) => ColorProgressBar();
        fantasylist.CollectionChanged += (sender, e) => ColorProgressBar();
        normallist.CollectionChanged += (sender, e) => ColorProgressBar();
    }

    public void ColorProgressBar()
    {
        progressBar.SetValue1(cyberlist.Count, animationSpeed);
        progressBar.SetValue2(fantasylist.Count, animationSpeed);
        progressBar.SetValue3(normallist.Count, animationSpeed);
    }

    public void SetResult()
    {
        switch (CompareValues(normallist.Count, fantasylist.Count, cyberlist.Count))
        {
            case 0:
                result.result = "normal";
                break;
            case 1:
                result.result = "fantasy";
                break;
            case 2:
                result.result = "cyber";
                break;
            case 3:
                result.result = "strange";
                break;
        }
    }

    public int CompareValues(float a, float b, float c)
{
    if (a > b && a > c)
    {
            return 0;
    }
    else if (b > a && b > c)
    {
            return 1;
    }
    else if (c > a && c > b)
    {
            return 2;
    }
    else
    {
            return 3;
    }
}

    void Update()
    {
        ColorProgressBar();
    }
}
