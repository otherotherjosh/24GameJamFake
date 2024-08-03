using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField][Range(0.1f, 1f)] private float timeBetweenSteps;
    [SerializeField] private AudioClip[] footsteps;
    private IEnumerator coroutine;
    private InputAction moveAction;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        float stepInterval = timeBetweenSteps;
        do
        {
            Debug.Log("Step");
            audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length - 1)]);
            if (InputSystem.actions.FindAction("Sprint").inProgress) stepInterval = timeBetweenSteps / 2;
            else stepInterval = timeBetweenSteps;
            yield return new WaitForSeconds(stepInterval);
        } while (moveAction.inProgress);
    }
}
