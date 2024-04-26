using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        // Horizontal follow with lookahead
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        float targetX = player.position.x + lookAhead;

        // Now simply include the player's y position in the camera's follow logic
        // Assuming you want the camera to directly follow the player's y position
        float targetY = player.position.y;

        // Update the camera's position
        // Keeping the original camera z position
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
