using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _cameraMoving;
    [SerializeField] private Animator _cameraAnim;

    private void MoveCamera(int indexCameraAnim)
    {
        _cameraAnim.SetFloat("Choose", indexCameraAnim);
        _cameraMoving.GetComponent<SpritePositioning>().AdjustSpritePositions();
    }
    private void Update()
    {
        _cameraMoving.GetComponent<SpritePositioning>().AdjustSpritePositions();
    }
}
