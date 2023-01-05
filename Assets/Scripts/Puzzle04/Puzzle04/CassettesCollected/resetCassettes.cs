using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCassettes : MonoBehaviour
{
    [SerializeField] private IntSO cassettesSO;
    // Start is called before the first frame update
    void Start()
    {
        cassettesSO.Value = 0;
    }
}
