using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Cube GetCreateCube(Cube cube, Transform transformObject)
    {
        Cube newCube = Instantiate(cube, transformObject.position, Quaternion.identity);

        return newCube;
    }
}
