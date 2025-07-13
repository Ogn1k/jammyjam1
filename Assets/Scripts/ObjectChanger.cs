
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class ObjectChanger : MonoBehaviour
{
    public TMP_Text objName;
    public GameObject button;
    public ObjectKeeper objectKeeper;
    public ObjectStater objectStater;
    private ObjectState objectState;
    public CurrentCustomObject currentCustomObject;
    public AudioMixerGroup mixer;
    public AudioClip[] changeSounds;
    private AudioSource audioSource;

    public void Start()
    {
        for (int i = 0; i < objectStater.objects.Count; i++)
        {
            if (objName.text == objectStater.objects[i].name)
            {
                objectState = objectStater.objects[i];
            }
        }
        ref string curState = ref objectKeeper.GetObjectByName(objName.text).state;
        //print(objName.text);
        button.transform.Find("Text").GetComponent<TMP_Text>().text = curState;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer;
    }

    public void ChangeState()
    {
        ref string curState = ref objectKeeper.GetObjectByName(objName.text).state;
        if (curState == "Normal")
        {
            curState = "Fantasy";
            //currentCustomObject.curObject = objectState.state_Fantasy;
            SetObjectFantasy();
        }
        else if (curState == "Fantasy")
        {
            curState = "Cyber";
            //currentCustomObject.curObject = objectState.state_Cyber;
            SetObjectCyber();
        }
        else if (curState == "Cyber")
        {
            curState = "Normal";
            //currentCustomObject.curObject = objectState.state_Normal;
            SetObjectNormal();
        }
        //print(objName.text);
        button.transform.Find("Text").GetComponent<TMP_Text>().text = curState;
    }

    public void SetObjectNormal()
    {
        Vector3 position = currentCustomObject.curObject.transform.position;
        //Quaternion rotation = currentCustomObject.curObject.transform.rotation;

        audioSource.PlayOneShot(changeSounds[0]);
        Destroy(currentCustomObject.curObject);
        currentCustomObject.curObject = Instantiate(objectState.state_Normal, position, objectState.state_Normal.transform.rotation);
    }
    public void SetObjectFantasy()
    {
        Vector3 position = currentCustomObject.curObject.transform.position;
        //Quaternion rotation = currentCustomObject.curObject.transform.rotation;

        audioSource.PlayOneShot(changeSounds[1]);
        Destroy(currentCustomObject.curObject);
        currentCustomObject.curObject = Instantiate(objectState.state_Fantasy, position, objectState.state_Fantasy.transform.rotation);
    }
    public void SetObjectCyber()
    {
        Vector3 position = currentCustomObject.curObject.transform.position;
        //Quaternion rotation = currentCustomObject.curObject.transform.rotation;
        
        audioSource.PlayOneShot(changeSounds[2]);
        Destroy(currentCustomObject.curObject);
        currentCustomObject.curObject = Instantiate(objectState.state_Cyber, position, objectState.state_Cyber.transform.rotation);
    }
}