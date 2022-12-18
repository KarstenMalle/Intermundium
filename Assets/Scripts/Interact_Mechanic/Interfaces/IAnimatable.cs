using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimatable
{
    bool isAnimateable { get; set; }
    Animation Animator { get; }

}
