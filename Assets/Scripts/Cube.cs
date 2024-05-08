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
        float maxRandomValue = 101f;
        float minRandomValueQuantityCubes = 2f;
        float maxRandomValueQuantityCubes = 7f;
        float divider = 2f;

        float randomValueDivisionChance = Random.Range(minRandomValue, maxRandomValue);
        float randomValueQuantityCubes = Random.Range(minRandomValueQuantityCubes, maxRandomValueQuantityCubes);

        _exploder.IncreaseForceUltimateExplosion();
        _exploder.IncreaseRadiusUltimateExplosion();

        if (_divisionChance >= randomValueDivisionChance)
        {
            ReduceDivisionChance(divider);

            List<Rigidbody> cubesCreated = new();

            for (int i = 0; i < randomValueQuantityCubes; i++)
            {
                Cube newCube = _spawner.GetCreateCube(this, transform);

                ReduceScale(newCube.gameObject, divider);

                newCube._exploder.IncreaseForceUltimateExplosion();
                newCube._exploder.IncreaseRadiusUltimateExplosion();

                cubesCreated.Add(newCube._rigidbody);
            }

            ReduceScale(gameObject, divider);

            _colorer.PaintInRandomColor(_meshRenderer);

            foreach (var item in cubesCreated)
            {
                _exploder.ExplodeCreatedObjects(item, transform);
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
