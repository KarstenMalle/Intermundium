using System;
using UnityEngine;

public class CandleEffect : MonoBehaviour
{
    Light candleLight;

    [Tooltip("Offset effect so candles aren't in sync")]
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        candleLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        double sin = Math.Abs(Math.Sin(Time.time + offset)) * 0.5 + 1;
        candleLight.intensity = (float)sin;
    }
}
