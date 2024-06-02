using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeThrower : MonoBehaviour
{
    [SerializeField] GameObject bladePrefab;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] Vector2 bladeThrowForce;
    [SerializeField] float bladeThrowDelay;
    [SerializeField] bool isBladeDestroy;
    [SerializeField] float bladeDestroyTime;

    void Start()
    {
        if (!SpawnPoint)
            SpawnPoint = transform;

        StartCoroutine(SpawnBlade());
    }

    IEnumerator SpawnBlade()
    {
        var blade = Instantiate(bladePrefab, SpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        blade.AddForce(bladeThrowForce);

        if (isBladeDestroy)
            Destroy(blade.gameObject, bladeDestroyTime);

        yield return new WaitForSeconds(bladeThrowDelay);
        StartCoroutine(SpawnBlade());
    }
}