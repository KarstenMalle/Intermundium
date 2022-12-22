using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IsAudible
{
    protected AudioClip audioClip { get; }
    protected bool IsAudible { get; set; }
}
