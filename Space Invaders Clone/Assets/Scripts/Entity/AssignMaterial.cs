using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignMaterial : MonoBehaviour
{
    private MeshRenderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        //_renderer.material = DownloadAssets.instance.MaterialToChange;
        _renderer.materials[0] = DownloadAssets.instance.MaterialToChange;
    }
}
