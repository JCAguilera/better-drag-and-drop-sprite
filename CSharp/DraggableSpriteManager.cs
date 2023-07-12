using System;
using System.Collections.Generic;
using System.Linq;

using Godot;

public partial class DraggableSpriteManager : Node
{
	// Made with love by JuanCarlos "Juanky" Aguilera
	// Github Repo: https://github.com/JCAguilera/godot3-drag-and-drop

	private List<DraggableSprite> sprites = new();
	private DraggableSprite topSprite;

	private RichTextLabel status;
	private RichTextLabel spriteList;
	
	public override void _Ready()
	{
		status = GetNode<RichTextLabel>("Status");
		spriteList = GetNode<RichTextLabel>("SpriteList");
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (Input.IsActionJustPressed("left_click")) //When we click
		{
			topSprite = _TopSprite(); // Get the sprite on Top (largest zIndex)
			if (topSprite != null) // If there's a sprite
				topSprite.dragging = true; // We set dragging to true
		}

		if (Input.IsActionJustReleased("left_click")) //When we release
		{
			if (topSprite != null)
			{
				topSprite.dragging = false; //Set dragging to false
				topSprite = null; // Top sprite to null
			}
		}
		_PrintStatus();
	}
	
	// Add sprite to list
	public void _AddSprite(DraggableSprite sprite)
	{   
		if (sprites.Contains(sprite))  // If sprite exists
			return;  // Do nothing
		
		sprites.Add(sprite);  // Add sprite to list
	}
	
	// Remove sprite from the list
	public void _RemoveSprite(DraggableSprite sprite)
	{
		sprites.Remove(sprite);
	}
	
	// Get the top sprite
	public DraggableSprite _TopSprite()
	{
		if (!sprites.Any())  // If the list is empty
			return null;

		return sprites.OrderByDescending(sprite => sprite.ZIndex).First(); // Return top sprite, sorted by ZIndex
	}
	
	public void _PrintStatus()
	{  
		List<int> auxSprt = new List<int>();
		List<bool> auxSprtCanDrag = new List<bool>();
		
		foreach(var i in sprites)
			auxSprt.Add(i.ZIndex);
		
		foreach(var i in sprites)
			auxSprtCanDrag.Add(i.dragging);
		
		if (topSprite != null)
			status.Text = "Top: " + topSprite.ZIndex + " - Dragging: " + topSprite.dragging;
		else
			status.Text = "Top: null - Dragging: False";
			
		spriteList.Text = "Sprites: " + string.Join(',', auxSprt) + " - Can drag: " + string.Join(',', auxSprtCanDrag);
	}
}