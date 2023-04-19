using Godot;
using System;

public partial class Item : Node2D
{
    [Export(PropertyHint.ResourceType, "ItemResource")]
    public ItemResource ItemResource;

    private Sprite2D _sprite;
    private ColorRect _colorRect;
    private Area2D _dragArea;
    private CollisionShape2D _dragAreaCollisionShape;
    private Marker2D _centerOfBaseSlot;


    private bool _selected = false;
    private const float LerpFactor = 30.0f;
    private const int SlotSize = 16;
    private Vector2 _target;

    private Tween _alphaTween;

    public override void _Ready()
    {
        base._Ready();
        _sprite = GetNode<Sprite2D>("Sprite2D");
        _colorRect = GetNode<ColorRect>("ColorRect");
        _dragArea = GetNode<Area2D>("Area2D");
        _centerOfBaseSlot = GetNode<Marker2D>("CenterOfBaseSlot");

        _dragAreaCollisionShape = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");

        _sprite.Texture = ItemResource.Sprite as Texture2D;
        _sprite.Scale = new Vector2(1 * ItemResource.Width, 1 * ItemResource.Height);

        _colorRect.Size = new Vector2(ItemResource.Width * SlotSize, ItemResource.Height * SlotSize);

        SetCorrectSizedCollisionShape();

        GD.Print(_dragArea);

        _dragArea.InputEvent += HandleInputEvent;

        _target = GlobalPosition;
    }

    private void SetCorrectSizedCollisionShape()
    {
        RectangleShape2D collisionRectangleShape2D = new RectangleShape2D();
        collisionRectangleShape2D.Size = new Vector2(ItemResource.Width * SlotSize, ItemResource.Height * SlotSize);
        if (ItemResource.Width > 1)
        {
            var offset = (ItemResource.Width - 1) * SlotSize;
            _dragArea.GlobalPosition += new Vector2(offset, 0);
        }

        if (ItemResource.Height > 1)
        {
            var offset = (ItemResource.Height - 1) * SlotSize;
            _dragArea.GlobalPosition += new Vector2(0, offset);
        }

        _dragAreaCollisionShape.Shape = collisionRectangleShape2D;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (_selected)
        {
            // - (_centerOfBaseSlot.Position * Scale) will ensure that the item always lerps to the position of the
            // first (top left) slot of an item, no matter how big it is.
            GlobalPosition =
                GlobalPosition.Lerp(GetViewport().GetMousePosition() - (_centerOfBaseSlot.Position * Scale),
                    (float)(LerpFactor * delta));
        }
        else
        {
            GlobalPosition =
                GlobalPosition.Lerp(_target,
                    (float)(10 * delta));
        }
    }

    private void HandleInputEvent(Node vp, InputEvent @event, long idx)
    {
        if (@event.IsActionPressed("click"))
        {
            GD.Print("Selected = True");
            _selected = true;
            ZIndex = 1;
            _alphaTween = GetTree().CreateTween();
            _alphaTween.TweenProperty(this, "modulate", new Color("ffffff7c"), 0.2);
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionReleased("click"))
        {
            _selected = false;
            ZIndex = 0;
            _alphaTween = GetTree().CreateTween();
            _alphaTween.TweenProperty(this, "modulate", new Color("ffffffff"), 0.2);
        }
    }
}