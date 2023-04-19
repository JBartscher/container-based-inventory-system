using Godot;
using System.Diagnostics;

public partial class Container : Node2D
{
    [ExportGroup("Container Dimensions")] [Export(PropertyHint.Range, "0,20,1")]
    public int CellWidth = 1;

    [Export(PropertyHint.Range, "0,20,1")] public int CellHeight = 1;

    [ExportGroup("Container Slot")] [Export(PropertyHint.NodeType)]
    public PackedScene SlotScene;

    private const int SlotSize = 16;

    public override void _Ready()
    {
        Debug.Assert(SlotScene != null);

        base._Ready();
        for (int y = 0; y < CellHeight; y++)
        {
            for (int x = 0; x < CellWidth; x++)
            {
                var slot = SlotScene.Instantiate() as Node2D;
                slot.GlobalPosition = new Vector2(GlobalPosition.X + (x * SlotSize), GlobalPosition.Y + (y * SlotSize));
                AddChild(slot);
            }
        }
    }
}