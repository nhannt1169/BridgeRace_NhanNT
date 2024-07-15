using UnityEngine;
using static Utils;

public class BridgeBrick : MonoBehaviour
{
    public ColorType Color { get; private set; }
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private CheckPoint checkPoint;
    [SerializeField] private GameObject wall;
    [SerializeField] ColorData colorData;

    private void Awake()
    {
        Color = ColorType.blank;
    }

    private void SetColor(ColorType color)
    {
        Color = color;
        mesh.material = colorData.GetMat(Color);
    }

    public void ChangeWallStatus(bool status)
    {
        wall.SetActive(status);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Character>(out var character))
        {
            if (character.Color != Color && character.HasBrick)
            {
                character.RemoveBrick();
                SetColor(character.Color);
            }
        }
    }
}
