using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefub;
    [SerializeField] private Explosion _explosion;

    private int _scaleReductionPerGeneration = 2;
    private int _dividePartsMin = 2;
    private int _dividePartsMax = 6;

    private void Start()
    {
        Cube startCube = Instantiate(_cubePrefub);
        startCube.Cliñked += DestroyCube;
        startCube.transform.position = this.transform.position;
    }

    public void Division(Cube parentCube)
    {
        Cube[] newCubes = new Cube[Random.Range(_dividePartsMin, _dividePartsMax)];
        int childrenGeneration = parentCube.Generation + 1;
        Vector3 parentPosition = parentCube.transform.position;
        Vector3 childrenScale = Vector3.one *
            ((float)_scaleReductionPerGeneration / (float)(childrenGeneration * _scaleReductionPerGeneration));

        for (int i = 0; i < newCubes.Length; i++)
        {
            newCubes[i] = Instantiate(_cubePrefub);
            newCubes[i].Cliñked += DestroyCube;
            newCubes[i].Initialize(childrenGeneration, parentPosition, childrenScale);
            newCubes[i].GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        }

        _explosion.Initiate(newCubes);
    }

    public void DestroyCube(Cube cube)
    {
        cube.Cliñked -= DestroyCube;
        Destroy(cube.gameObject);
    }
}