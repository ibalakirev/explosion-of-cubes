using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private float _divisionChance = 100;
    private float _forceExplosion = 100f;
    private float _radiusExplosion = 100f;

    private void OnEnable()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        PaintInRandomColor();
    }

    private void OnMouseDown()
    {
        float minRandomValue = 0f;
        float maxRandomValue = 101f;
        float minRandomValueQuantityCubes = 2f;
        float maxRandomValueQuantityCubes = 7f;
        float divider = 2f;

        float randomValueDivisionChance = Random.Range(minRandomValue, maxRandomValue);
        float randomValueQuantityCubes = Random.Range(minRandomValueQuantityCubes, maxRandomValueQuantityCubes);

        if (_divisionChance >= randomValueDivisionChance)
        {
            _divisionChance /= divider;

            List<Rigidbody> cubesCreated = new();

            for (int i = 0; i < randomValueQuantityCubes; i++)
            {
                Cube cube = Instantiate(this, transform.position, Quaternion.identity);

                cube.transform.localScale /= divider;

                if(cube.TryGetComponent(out Rigidbody cubeRigidbody))
                {
                    cubesCreated.Add(cubeRigidbody);
                }
            }

            transform.localScale /= divider;

            PaintInRandomColor();

            foreach (var item in cubesCreated)
            {
                item.AddExplosionForce(_forceExplosion, transform.position, _radiusExplosion);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void PaintInRandomColor()
    {
        _meshRenderer.material.color = Random.ColorHSV();
    }
}
