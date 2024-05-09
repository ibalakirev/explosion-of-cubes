using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Cube Create(Cube cube, Transform transformObject)
    {
        return Instantiate(cube, transformObject.position, Quaternion.identity); ;
    }
}
