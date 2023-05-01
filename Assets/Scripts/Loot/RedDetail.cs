using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDetail : Details
{
    protected override void GetDetail(ShipStats shipStats)
    {
        shipStats.ApplyRdDetails(_countDetails);
        base.GetDetail(shipStats);
    }
}
