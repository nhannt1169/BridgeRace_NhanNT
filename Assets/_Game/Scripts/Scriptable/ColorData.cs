using UnityEngine;
using static Utils;

[CreateAssetMenu(menuName = "ColorData")]
public class ColorData : ScriptableObject
{
    [SerializeField] Material[] materials;

    public Material GetMat(ColorType colorType)
    {
        return materials[(int)colorType];
    }
}