using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject background;

    private Vector2 cameraMinPosition;
    private Vector2 cameraMaxPosition;

    public void Start()
    {
        Camera camera = GetComponent<Camera>();
        Vector2 cameraHalfSize = CalculateCameraHalfSizeInUnits(camera);

        Bounds backgroundBounds = background.GetComponent<SpriteRenderer>().bounds;

        cameraMinPosition = (Vector2)backgroundBounds.min + cameraHalfSize;
        cameraMaxPosition = (Vector2)backgroundBounds.max - cameraHalfSize;
    }

    private Vector2 CalculateCameraHalfSizeInUnits(Camera camera)
    {
        Vector3 cameraCenter = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
        Vector3 cameraBottomLeft = camera.ViewportToWorldPoint(Vector3.zero);

        return cameraCenter - cameraBottomLeft;
    }

    public void LateUpdate()
    {
        Vector2 newPosition = Vector2.Max(cameraMinPosition, player.transform.position);
        newPosition = Vector2.Min(cameraMaxPosition, newPosition);

        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
