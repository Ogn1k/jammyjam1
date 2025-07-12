using UnityEngine;

public class FootSound : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Массив звуков шагов
    public float stepDelay = 0.5f;     // Задержка между шагами
    public float minSpeed = 0.1f;      // Минимальная скорость для шагов
    public float maxSpeed = 10f;
    private AudioSource audioSource;
    private float nextStepTime;
    public CharacterController characterController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Проверяем, движется ли игрок и стоит ли он на земле
        bool isMoving = characterController.velocity.magnitude > minSpeed;
        bool isSprinting = characterController.velocity.magnitude > maxSpeed;
        bool isGrounded = characterController.isGrounded;

        if (!isSprinting && isMoving && isGrounded && Time.time >= nextStepTime)
        {
            PlayFootstepSound();
            nextStepTime = Time.time + stepDelay;
        }
        else if (isSprinting && isGrounded && Time.time >= nextStepTime)
        {
            PlayFootstepSound();
            nextStepTime = Time.time + stepDelay-0.3f;
        }
    }

    void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0) return;

        // Выбираем случайный звук из массива
        int randomIndex = Random.Range(0, footstepSounds.Length);
        audioSource.PlayOneShot(footstepSounds[randomIndex]);
    }
}