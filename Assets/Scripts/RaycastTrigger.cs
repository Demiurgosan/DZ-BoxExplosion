using System;
using UnityEngine;

public class RaycastTrigger : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private int _leftMouseButtonKey = 0;

    public event Action <RaycastHit> CubeDetected;

    void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButtonKey))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out ExplosiveCube cube))
                    {
                        CubeDetected.Invoke(hit);
                    }
                }
            }
        }
    }
}
