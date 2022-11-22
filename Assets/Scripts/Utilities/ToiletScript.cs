using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletScript : MonoBehaviour
{
    private SkinnedMeshRenderer _mesh;

    private void Awake()
    {
        _mesh = GetComponent<SkinnedMeshRenderer>();
        _mesh.SetBlendShapeWeight(1, 100);
    }
}
