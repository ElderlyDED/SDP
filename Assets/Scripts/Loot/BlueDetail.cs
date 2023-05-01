using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDetail : Details
{
    protected override void GetDetail(ShipStats shipStats)
    {
        shipStats.ApplyBlueDetails(_countDetails);
        base.GetDetail(shipStats);
    }
}
