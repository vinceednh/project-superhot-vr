using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public CharacterController playerController;

    public float movingTimeScale = 1f;
    public float idleTimeScale = 0.1f;
    public float smooth = 5f;

    float currentScale;
    Vector3 lastPosition;

    void Start()
    {
        if (playerController == null)
        {
            Debug.LogError("Player Controller not assigned!");
            return;
        }

        lastPosition = playerController.transform.position;
    }

    void Update()
    {
        // measure actual movement
        float distanceMoved = Vector3.Distance(
            playerController.transform.position,
            lastPosition
        );

        lastPosition = playerController.transform.position;

        bool isMoving = distanceMoved > 0.001f;

        float target = isMoving ? movingTimeScale : idleTimeScale;

        currentScale = Mathf.Lerp(currentScale, target, Time.deltaTime * smooth);

        Time.timeScale = currentScale;
        Time.fixedDeltaTime = 0.02f * currentScale;
    }
}