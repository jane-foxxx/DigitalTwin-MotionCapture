using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vGear;
using Votanic.vXR.vGear.Inventory;

public class vGearInventoryManagerTemp : vGear_InventoryManager
{
    public override void AddItems()
    {

    }

    public override void Apply(vGear_ItemContainer container, vGear_Controller controller)
    {
        base.Apply(container, controller);
    }

    public override void SuccessLog(SuccessType msg, Item item)
    {

    }

    public override void ErrorLog(ErrorType msg)
    {

    }
}
