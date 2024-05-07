using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody), typeof(Colorer))]

public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Colorer _colorer;
    private MeshRenderer _meshRenderer;
    private float _divisionChance = 100;
    private float _forceExplosion = 100f;
    private float _radiusExplosion = 100f;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _colorer = GetComponent<Colorer>();
    }

    private void OnEnable()
    {
        _colorer.PaintInRandomColor(_meshRenderer);
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
            ReduceDivisionChance(divider);

            List<Rigidbody> cubesCreated = new();

            for (int i = 0; i < randomValueQuantityCubes; i++)
            {
                Cube cube = Instantiate(this, transform.position, Quaternion.identity);

                ReduceScale(cube.gameObject, divider);

                cubesCreated.Add(_rigidbody);
            }

            ReduceScale(gameObject, divider);

            _colorer.PaintInRandomColor(_meshRenderer);

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

    private void ReduceScale(GameObject cube, float divider)
    {
        cube.transform.localScale /= divider;
    }

    private void ReduceDivisionChance(float divider)
    {
        _divisionChance /= divider;
    }
}
