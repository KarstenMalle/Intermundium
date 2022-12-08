using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPages : MonoBehaviour
{
    [SerializeField] private IntSO pagesSO;
    // Start is called before the first frame update
    void Start()
    {
        pagesSO.Value = 0;
    }
}
