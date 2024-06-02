using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : ObstacleController
{

    void Update() => transform.Rotate(Vector3.forward * Time.deltaTime * 90);
}
