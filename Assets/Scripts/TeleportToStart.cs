using UnityEngine;

public class TeleportToStart : MonoBehaviour
{
    [SerializeField] BoxCollider _floor;
    [SerializeField] Transform _player;

    Vector3 startPosition;
    private void Start()
    {
        startPosition = _player.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Death")
        {
            _player.position = startPosition;
        }
    }

}
