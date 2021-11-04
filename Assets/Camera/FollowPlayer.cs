using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    public Vector2 bottomLeftLimit;
    public Vector2 topRightLimit;

    private Vector3 lastPlayerPosition;

    private Camera camera;

    public void Start()
    {
        camera = gameObject.GetComponent<Camera>();

        Debug.Log(
            bottomLeftLimit + camera.pixelRect.size / 2
        );
    }

    //public void LateUpdate()
    //{
    //    Vector3 playerPosition = player.transform.position;

    //    if (lastPlayerPosition == playerPosition)
    //    {
    //        return;
    //    }

    //    lastPlayerPosition = playerPosition;

    //    Vector2 newPosition = Vector2.Max(bottomLeftLimit, playerPosition);
    //    newPosition = Vector2.Min(newPosition, topRightLimit);

    //    gameObject.transform.position = new Vector3(
    //        newPosition.x,
    //        newPosition.y,
    //        gameObject.transform.position.z
    //    );
    //}
}
