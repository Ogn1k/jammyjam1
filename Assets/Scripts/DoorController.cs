using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator animator;
    private bool playerIsNear;

    // Вызывается, когда игрок входит в триггер
    private void OnTriggerEnter(Collider other)
    {
        
            playerIsNear = true;
            animator.SetBool("IsOpening", true); // Запуск анимации открытия
        
    }

    // Вызывается, когда игрок выходит из триггера
    private void OnTriggerExit(Collider other)
    {
        
            playerIsNear = false;
            animator.SetBool("IsOpening", false); // Запуск анимации закрытия
        
    }
}
