
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public Camera MainCamera { get; private set; }

    private Transform target;
    private Vector3 offset;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            MainCamera = Camera.main;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target, Vector3 offset)
    {
        this.target = target;
        this.offset = offset;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            MainCamera.transform.position = target.position + offset;
        }

        if (shakeDuration > 0)
        {
            MainCamera.transform.position += Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
        }
    }

    public void CameraShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    public void SwitchCamera(Camera newCamera)
    {
        MainCamera.enabled = false;
        newCamera.enabled = true;
        MainCamera = newCamera;
    }
}
