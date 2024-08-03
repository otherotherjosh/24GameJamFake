using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField][Range(0.1f, 1f)] private float timeBetweenSteps;
    [SerializeField][Range(0, 1)] float pitchVariation;
    [SerializeField][Range(0, 1)] private float sprintVolumeBoost;
    [SerializeField] private AudioClip[] footsteps;
    private float volume;
    private IEnumerator coroutine;
    private InputAction moveAction;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        volume = audioSource.volume;
        moveAction = InputSystem.actions.FindAction("Move");
        coroutine = footstepRoutine();
        moveAction.started += ctx => StartCoroutine(coroutine);
        moveAction.canceled += ctx =>
        {
            StopCoroutine(coroutine);
            Debug.Log("Stopping Coroutine???");
        };
    }

    IEnumerator footstepRoutine()
    {
        float stepInterval;
        do
        {
            Debug.Log("Step");
            audioSource.pitch = Random.Range(1 - pitchVariation / 2, 1 + pitchVariation / 2);
            audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length - 1)]);
            if (InputSystem.actions.FindAction("Sprint").inProgress) 
            {
                stepInterval = timeBetweenSteps * 0.7f;
                audioSource.volume = volume + sprintVolumeBoost;
            }
            else 
            {
                stepInterval = timeBetweenSteps;
                audioSource.volume = volume;
            }
            yield return new WaitForSeconds(stepInterval);
        } while (moveAction.inProgress);
    }
}
