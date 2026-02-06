using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 Offset = new Vector3(0, 1, -10);

    private void FixedUpdate()
    {
        transform.position = new Vector3(0, Player.transform.position.y, 0) + Offset;
    }
}
