using Godot;

public partial class DraggableSprite : Sprite2D
{
	public bool mouseIn;
	public bool dragging = false;
	public bool mouseToCenterSet = false;
	public Vector2 mouseToCenter;
	public Vector2 spritePos;
	public Vector2 mousePos;

	public override void _UnhandledInput(InputEvent @event)
	{
		if (!Input.IsActionJustPressed("left_click") || !mouseIn) // When clicking
			return;

		// First we set mouseToCenter as a static vector
		// for preventing the sprite to move its center to the mouse position
		mousePos = GetViewport().GetMousePosition();
		mouseToCenter = RestaVectores(Position, mousePos);
	}
	
	public override void _Process(double _delta)
	{
		if (!dragging)
			return;

		mousePos = GetViewport().GetMousePosition();
		//Set the position of the sprite to mouse position + static mouseToCenter vector
		Position = SumaVectores(mousePos, mouseToCenter);
	}
	
	public void _OnArea2DMouseEntered()
	{
		mouseIn = true;
		(GetParent() as DraggableSpriteManager)?._AddSprite(this) ;//Add the sprite to the sprite list
	}
	
	public void _OnArea2DMouseExited()
	{
		mouseIn = false;
		(GetParent() as DraggableSpriteManager)?._RemoveSprite(this)  ;//Remove the sprite from the sprite list
	}
	
	public Vector2 RestaVectores(Vector2 v1, Vector2 v2)
	{
		return new Vector2(v1.X - v2.X, v1.Y - v2.Y);  //vector substraction
	}
	
	public Vector2 SumaVectores(Vector2 v1, Vector2 v2)
	{   
		return new Vector2(v1.X + v2.X, v1.Y + v2.Y);  //vector sum
	}
}