using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private BridgeBrick brick;
    private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.playerTag))
        {
            if (player == null)
            {
                player = other.GetComponent<Player>();
            }
            if (player.HasBrick || brick.Color == player.Color)
            {
                brick.ChangeWallStatus(false);
            }
            else
            {
                brick.ChangeWallStatus(true);
            }
        }
    }
}
