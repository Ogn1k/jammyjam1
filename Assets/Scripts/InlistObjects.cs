using System.Collections.Generic;
using UnityEngine;

public class InlistObjects : MonoBehaviour
{

    public LvlLogic lvlLogic;

    // Вызывается при включении объекта (добавлении на сцену)
    void OnEnable()
    {
        lvlLogic = GameObject.Find("LvlLogic").GetComponent<LvlLogic>();
        if (gameObject.CompareTag("cyber"))
        {
            if (!lvlLogic.cyberlist.Contains(gameObject))
            {
                lvlLogic.cyberlist.Add(gameObject);
                //Debug.Log($"Добавлен объект: {gameObject.name}");
            }
        }
        if (gameObject.CompareTag("normal"))
        {
            if (!lvlLogic.normallist.Contains(gameObject))
            {
                lvlLogic.normallist.Add(gameObject);
                //Debug.Log($"Добавлен объект: {gameObject.name}");
            }
        }
        if (gameObject.CompareTag("fantasy"))
        {
            if (!lvlLogic.fantasylist.Contains(gameObject))
            {
                lvlLogic.fantasylist.Add(gameObject);
                //Debug.Log($"Добавлен объект: {gameObject.name}");
            }
        }
    }

    // Вызывается при выключении/удалении объекта
    void OnDisable()
    {
        if (gameObject.CompareTag("cyber"))
        {
            if (lvlLogic.cyberlist.Contains(gameObject))
            {
                lvlLogic.cyberlist.Remove(gameObject);
                //Debug.Log($"Удалён объект: {gameObject.name}");
            }
        }
        if (gameObject.CompareTag("normal"))
        {
            if (lvlLogic.normallist.Contains(gameObject))
            {
                lvlLogic.normallist.Remove(gameObject);
                //Debug.Log($"Удалён объект: {gameObject.name}");
            }
        }
        if (gameObject.CompareTag("fantasy"))
        {
            if (lvlLogic.fantasylist.Contains(gameObject))
            {
                lvlLogic.fantasylist.Remove(gameObject);
                //Debug.Log($"Удалён объект: {gameObject.name}");
            }
        }
    }
}
