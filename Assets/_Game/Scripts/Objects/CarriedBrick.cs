using UnityEngine;
using static Utils;

public class CarriedBrick : GameUnit
{
    public ColorType Color { get; private set; }
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] ColorData colorData;

    public void SetColor(ColorType color)
    {
        Color = color;
        mesh.material = colorData.GetMat(Color);
    }
}
