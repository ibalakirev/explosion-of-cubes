using UnityEngine;

public class Colorizer : MonoBehaviour
{
    public void PaintInRandomColor(MeshRenderer meshRenderer)
    {
        meshRenderer.material.color = Random.ColorHSV();
    }
}
