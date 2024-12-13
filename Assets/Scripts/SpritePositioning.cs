using UnityEngine;

public class SpritePositioning : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Vector2 referenceResolution = new Vector2(1920, 1080);

    private void Awake() => AdjustSpritePositions();
 
    public void AdjustSpritePositions()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float targetAspect = referenceResolution.x / referenceResolution.y;

        float scaleFactor = currentAspect / targetAspect;

        if (m_Camera.orthographic)
            m_Camera.orthographicSize /= scaleFactor;

        foreach (Transform child in transform)
        {
            Vector3 originalPosition = child.localPosition;
            child.localPosition = new Vector3(originalPosition.x * scaleFactor, originalPosition.y, originalPosition.z);
        }
    }
}
