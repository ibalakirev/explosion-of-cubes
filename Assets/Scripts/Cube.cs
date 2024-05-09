using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody), typeof(Colorizer))]
[RequireComponent(typeof(Spawner), typeof(Exploder))]

public class Cube : MonoBehaviour
{
    private Exploder _exploder;
    private Spawner _spawner;
    private Rigidbody _rigidbody;
    private Colorizer _colorer;
    private MeshRenderer _meshRenderer;
    private float _divisionChance = 100;
    private Transform _currentPosition;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _spawner = GetComponent<Spawner>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _colorer = GetComponent<Colorizer>();
    }

    private void OnEnable()
    {
        _colorer.PaintInRandomColor(_meshRenderer);
    }

    private void OnDisable()
    {
        _currentPosition = transform;

        _exploder.ExplodeAllCubesInRadius(_currentPosition);
    }

    private void OnMouseDown()
    {
        float minRandomValue = 0f;
        float maxRandomValue = 100f;
        float minRandomValueQuantityCubes = 2f;
        float maxRandomValueQuantityCubes = 6f;
        float divider = 2f;

        float randomValueDivisionChance = Random.Range(minRandomValue, maxRandomValue);
        float randomValueQuantityCubes = Random.Range(minRandomValueQuantityCubes, maxRandomValueQuantityCubes);

        if (_divisionChance >= randomValueDivisionChance)
        {
            List<Rigidbody> cubesCreated = new();

            _exploder.ExplodeCurrentObject(_rigidbody, transform);

            _colorer.PaintInRandomColor(_meshRenderer);

            ReduceDivisionChance(divider);

            for (int i = 0; i < randomValueQuantityCubes; i++)
            {
                Cube newCube = _spawner.Create(this, transform);

                ReduceScale(newCube.gameObject, divider);

                IncreaseExplosionCharacteristics(newCube);

                cubesCreated.Add(newCube._rigidbody);
            }

            ReduceScale(gameObject, divider);

            foreach (var item in cubesCreated)
            {
                _exploder.ExplodeCurrentObject(item, transform);
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

    private void IncreaseExplosionCharacteristics(Cube cube)
    {
        cube._exploder.IncreaseForceUltimateExplosion();
        cube._exploder.IncreaseRadiusUltimateExplosion();
    }
}
