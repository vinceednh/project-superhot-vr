using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float movingTimeScale = 1f;
    public float idleTimeScale = 0.3f;
    public float velocityThreshold = 0.15f;
    public float accelerateSpeed = 4f;
    public float decelerateSpeed = 1.5f;

    float currentScale;

    void Start()
    {
        currentScale = idleTimeScale;
        Time.timeScale = idleTimeScale;
        Time.fixedDeltaTime = 0.02f * idleTimeScale;
    }

    void Update()
    {
        Vector3 leftVel = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
        Vector3 rightVel = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);

        float maxVel = Mathf.Max(leftVel.magnitude, rightVel.magnitude);
        bool isMoving = maxVel > velocityThreshold;

        float target = isMoving ? movingTimeScale : idleTimeScale;
        float speed = isMoving ? accelerateSpeed : decelerateSpeed;

        currentScale = Mathf.MoveTowards(currentScale, target, speed * Time.unscaledDeltaTime);

        Time.timeScale = currentScale;
        Time.fixedDeltaTime = 0.02f * currentScale;
    }
}