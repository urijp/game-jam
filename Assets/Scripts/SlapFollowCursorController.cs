using System.Collections;
using UnityEngine;

public class SlapFollowCursorController : MonoBehaviour
{
    private Camera _camera;
    private bool _isSlapping;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Slapping());
        }

        if(_isSlapping)
        {
            ShowCursor();
        }
        else
        {
            transform.position = new Vector3(0,0,-10);
        }
    }

    private void ShowCursor()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = _camera.transform.position.z + _camera.nearClipPlane;
        transform.position = mousePosition;
    }

    private IEnumerator Slapping()
    {
        _isSlapping = true;
        yield return new WaitForSeconds(0.2f);
        _isSlapping = false;
    }
}
