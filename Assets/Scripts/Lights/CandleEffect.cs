using System;
using UnityEngine;

public class CandleEffect : MonoBehaviour
{
    Light candleLight;

    [Tooltip("Offset effect so candles aren't in sync")]
    public float offset;

    [Tooltip("How much the candle will sway")]
    public double sway = 1d;

    [Tooltip("How fast the candle will sway")]
    public float speed = 1f;

    private float init_intensity;

    // Start is called before the first frame update
    void Start()
    {
        candleLight = GetComponent<Light>();
        init_intensity = candleLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        double sin = Math.Sin(Time.time * speed + offset) * 0.5 * sway + init_intensity;
        candleLight.intensity = (float)sin;
    }
}
