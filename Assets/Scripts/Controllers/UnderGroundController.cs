using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UnderGroundController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject Background;
    [SerializeField] Light2D PlayerLight;
    [SerializeField] Light2D SunLight;
    [SerializeField] float UndergroundLimit;

    void Update()
    {
        if (player.position.y <= UndergroundLimit)
        {
            PlayerLight.intensity = Mathf.Clamp((Mathf.Abs(UndergroundLimit - player.position.y) * .15f), 0, .5f);
            SunLight.intensity = Mathf.Clamp(1 - (Mathf.Abs(UndergroundLimit - player.position.y) * .3f), 0, 1);
        }
        else
        {
            PlayerLight.intensity = 0;
            SunLight.intensity = 1;
        }
    }
}
