using Godot;
using System;

public partial class SlotData : Resource
{
    private const int MAX_STACK_SIZE = 64;

    [Export] public ItemResource Item { get; set; }

    [Export(PropertyHint.Range, "1,64, 1")]
    public int quantity = 1;
}