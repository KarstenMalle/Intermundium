using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReadable
{
    bool IsReadable { get; set; }
    string text { get; set; }
}
