using UnityEngine;

public class Colorer : MonoBehaviour
{
    public void PaintInRandomColor(MeshRenderer meshRenderer)
    {
        meshRenderer.material.color = Random.ColorHSV();
    }
}
