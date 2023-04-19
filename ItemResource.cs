using Godot;
using System;
using MonoCustomResourceRegistry;

[RegisteredType(nameof(ItemResource), "", nameof(Resource))]
public partial class ItemResource : Resource
{
    [Export] public String Name { get; set; }
    [Export] public String Description { get; set; }
    [Export] public Boolean Stackable { get; set; }
    [Export] public Texture Sprite { get; set; }
    [Export] public int Width { get; set; }
    [Export] public int Height { get; set; }


    public ItemResource()
    {
        Name = "default name";
        Description = "default description";
        Stackable = false;
        Sprite = null;
        Width = 1;
        Height = 1;
    }
}