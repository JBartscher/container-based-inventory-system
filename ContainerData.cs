using Godot;
using System;
using Godot.Collections;

public partial class SlotData : Resource
{
    [ExportGroup("Container Slots")] [Export(PropertyHint.ArrayType)]
    private Array<SlotData> slotDatas;
}