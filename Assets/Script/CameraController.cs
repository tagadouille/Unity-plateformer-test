using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cmCamera;

    private Transform playerTransform;
    
    void Start()
    {
        playerTransform = transform;
    }

    void Update()
    {
        // Change FOV when space is pressed :
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var lens = cmCamera.Lens;
            lens.FieldOfView = 70f;
            cmCamera.Lens = lens;
        }

        // Apply the rotation of the camera to the player :
        float cameraYRotation = cmCamera.transform.eulerAngles.y;
        
        playerTransform.rotation = Quaternion.Euler(0, cameraYRotation, 0);
    }
}