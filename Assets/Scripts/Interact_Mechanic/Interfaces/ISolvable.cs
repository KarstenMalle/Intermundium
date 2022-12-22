using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISolvable
{
    bool IsSolved { get; set; }

    string SolutionString { get; set; }
    
    Array SolutionArray { get; set; }
}
