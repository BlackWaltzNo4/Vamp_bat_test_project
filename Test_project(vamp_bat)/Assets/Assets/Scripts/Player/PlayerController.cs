using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;

    class PlayerState
    {
        public float yaw;
        public float x;
        public float y;
        public float z;

        private Vector3 previousPosition;

        public void SetFromTransform(Transform t)
        {
            yaw = t.eulerAngles.y;            
            x = t.position.x;
            y = t.position.y;
            z = t.position.z;            
        }

        public void Translate(Vector3 translation)
        {
            Vector3 rotatedTranslation = Quaternion.Euler(0, yaw, 0) * translation;

            x += rotatedTranslation.x;
            y += rotatedTranslation.y;
            z += rotatedTranslation.z;
        }

        public void LerpTowards(PlayerState target, float positionLerpPct, float rotationLerpPct)
        {
            yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);

            x = Mathf.Lerp(x, target.x, positionLerpPct);
            y = Mathf.Lerp(y, target.y, positionLerpPct);
            z = Mathf.Lerp(z, target.z, positionLerpPct);
        }

        public void UpdateTransform(Transform t)
        {
            t.eulerAngles = new Vector3(0, yaw, 0);
            t.GetComponent<CharacterController>().Move((new Vector3(x, y, z) - previousPosition));
            previousPosition = new Vector3(x, y, z);

            Debug.Log("Move vector: " + (new Vector3(x,y,z) - previousPosition) + ", Previous position: " + previousPosition);
        }
    }

    PlayerState m_targetPlayerState = new PlayerState();
    PlayerState m_interpolatingPlayerState = new PlayerState();

    [Header("Movement Settings")]
    [Tooltip("Exponential boost factor on translation, controllable by mouse wheel.")]
    public float boost = 3.5f;

    [Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
    public float positionLerpTime = 0.2f;

    [Header("Rotation Settings")]
    [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
    public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

    [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
    public float rotationLerpTime = 0.01f;

    [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
    public bool invertY = false;

    void OnEnable()
    {
        Cursor.visible = false;

        m_targetPlayerState.SetFromTransform(transform);
        m_interpolatingPlayerState.SetFromTransform(transform);
    }

    Vector3 GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }        
        return direction;
    }

    float GetInputRotation()
    {
        float rotation = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            rotation = -100f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotation = 100f;
        }        
        return rotation;
    }

    void Update()
    {
        //Rotation X
        var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));
        var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);
        m_targetPlayerState.yaw += (mouseMovement.x * mouseSensitivityFactor + GetInputRotation() * Time.deltaTime);

        //Rotation Y
        float rotY = mouseMovement.y * mouseSensitivityFactor;
        var cameraTransform = playerCamera.transform;
        var eulerY = cameraTransform.eulerAngles.y;

        cameraTransform.Rotate(new Vector3(rotY, 0, 0));
        var eulerTiltX = cameraTransform.eulerAngles.x;

        Debug.Log("EulerX = " + eulerTiltX + ", EulerY = " + eulerY);

        if (eulerTiltX <= 180f)
        {
            eulerTiltX = Mathf.Clamp(eulerTiltX, 0f, 50f);
        }
        else if (eulerTiltX > 180f)
        {
            eulerTiltX = Mathf.Clamp(eulerTiltX, 270f, 360f);
        }
        cameraTransform.eulerAngles = new Vector3(eulerTiltX, eulerY, 0);

        // Translation
        var translation = GetInputTranslationDirection() * Time.deltaTime;

        // Modify movement by a boost factor (defined in Inspector and modified in play mode through the mouse scroll wheel)
        boost += Input.mouseScrollDelta.y * 0.2f;
        translation *= Mathf.Pow(2.0f, boost);

        m_targetPlayerState.Translate(translation);

        // Framerate-independent interpolation
        // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
        var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
        var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
        m_interpolatingPlayerState.LerpTowards(m_targetPlayerState, positionLerpPct, rotationLerpPct);

        m_interpolatingPlayerState.UpdateTransform(transform);
    }
}
