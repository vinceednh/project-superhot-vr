using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float movingTimeScale = 1f;
    public float idleTimeScale = 0.05f;
    public float smooth = 5f;
    public float velocityThreshold = 0.02f;

    float currentScale;

    void Start()
    {
        currentScale = idleTimeScale;
    }

    void Update()
    {
        Vector3 leftVel = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
        Vector3 rightVel = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);

        float maxVel = Mathf.Max(leftVel.magnitude, rightVel.magnitude);
        bool isMoving = maxVel > velocityThreshold;

        float target = isMoving ? movingTimeScale : idleTimeScale;
        currentScale = Mathf.Lerp(currentScale, target, Time.unscaledDeltaTime * smooth);

        Time.timeScale = currentScale;
        Time.fixedDeltaTime = 0.02f * currentScale;
    }
}