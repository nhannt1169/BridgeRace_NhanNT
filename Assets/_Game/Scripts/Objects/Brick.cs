using UnityEngine;
using static Utils;

public class Brick : GameUnit
{
    public ColorType Color { get; private set; }
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private StageIdx stage;
    [SerializeField] ColorData colorData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Character>(out var character))
        {
            if (character.Color == Color)
            {
                character.AddBrick(Color);
                gameObject.SetActive(false);
                Invoke(nameof(SetActive), 5f);
            }
        }
    }

    private void SetActive()
    {
        gameObject.SetActive(true);
    }

    public void SetColor(ColorType color)
    {
        Color = color;
        mesh.material = colorData.GetMat(Color);
    }
}
