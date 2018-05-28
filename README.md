# Better Drag-and-drop Sprite
Easy way to drag and drop in Godot without using Collision Shapes. This is based on the [Drag-and-drop  Sprite](https://github.com/GodotGarden/drag-and-drop-sprite) example by [Godot Garden](https://github.com/GodotGarden).

## Improvements:

### Issue 1: Sprite's center moves to the mouse position when we click

First issue I found was that when I cliked a sprite to drag it, the center of the sprite moved to the mouse position. That causes some problems when trying to drag multiple sprites, and also, it didn't look good.
I fix it this way (using vectors):
![mouse_pos changes, but mouse to center is static, mouse_pos+mouse_to_center = sprite_pos](http://u.cubeupload.com/JuankyVader/387OperaInstantnea20180.png)mouse_pos, is where we "click" the sprite to move it and mouse_to_center is a static vector that represents the distance from where we are grabbing the sprite to the center of it.
To calculate this, I did this: First, when we click the sprite, we calculate the mouse_to_center using the sprite_pos (at the moment when we clicked). So, mouse_to_center = sprite_pos - mouse_pos.
Then, when dragging the sprite, we calculate the "new" sprite position as the mouse_position plus the mouse_to_center vector. That's it!

### Issue 2: Using multiple sprites (Overlapping sprites)

When we have multiple sprites in one place, we always need to move the sprite on the top. To calculate this, a lot of drag and drop examples use a picker object that is always following the mouse.
Now, that method requires the use of Collision Shapes and Area2D nodes, and I didn't want to use them. So I figured out another way to do it.
When the mouse enters a Sprites area, is added to a sprites array, so then, when we click, we order the list by z_index and get the top sprite that we want to move. Really easy!

## Help me improve this example

My brain was not working at a 100% when I made this example, so I think a lot of code can be improved. Please feel free to help me improve this example, so we can help more people.

## License

MIT License - see [LICENSE](LICENSE) for more details.
