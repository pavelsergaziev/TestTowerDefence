using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerSettings
{
    Sprite Sprite { get; }
    int BuildPrice { get; }
    float ShootRange { get; }
    float ShootInterval { get; }
    int Damage { get; }
}
