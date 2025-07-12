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

    void Update()
    {
        ColorProgressBar();
    }
}
