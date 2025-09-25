using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefub;

    private int _scaleReductionPerGeneration = 2;
    private int _dividePartsMin = 2;
    private int _dividePartsMax = 6;

    private void Start()
    {
        GameObject startCube = Instantiate(_cubePrefub);
        startCube.transform.position = this.transform.position;
        startCube.GetComponent<ExplosiveCube>().Destroing += DestroyCube;
    }

    public void MakeCubes(GameObject parentCube)
    {
        GameObject[] newCubes = new GameObject[Random.Range(_dividePartsMin, _dividePartsMax)];
        int childrenGeneration = parentCube.GetComponent<ExplosiveCube>().Generation + 1;
        Vector3 parentPosition = parentCube.transform.position;
        Vector3 childrenScale = Vector3.one *
            ((float)_scaleReductionPerGeneration / (float)(childrenGeneration * _scaleReductionPerGeneration));

        for (int i = 0; i < newCubes.Length; i++)
        {
            newCubes[i] = Instantiate(_cubePrefub);
            newCubes[i].GetComponent<ExplosiveCube>().Initialize(childrenGeneration, parentPosition, childrenScale);
            newCubes[i].GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            newCubes[i].GetComponent<ExplosiveCube>().Destroing += DestroyCube;
        }
    }

    public void DestroyCube(GameObject cube)
    {
        Destroy(cube);
    }
}