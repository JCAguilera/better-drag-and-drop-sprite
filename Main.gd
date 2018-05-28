# Made with love by JuanCarlos "Juanky" Aguilera
# Github Repo: https://github.com/JCAguilera/godot3-drag-and-drop

extends Node

var sprites = []
var top_sprite = null
var dragging = false
var top_set = false

onready var status = $Status
onready var spriteList = $SpriteList

func _process(delta):
	if Input.is_action_pressed("left_click"): #When we click
		if not top_set: #If there's no top we set one
			top_sprite = _top_sprite() #Get the sprite on top (largest z_index)
			if not top_sprite == null: #If there's a sprite
				top_sprite.can_drag = true #We set can_drag to true
			top_set = true
		dragging = true #We are dragging now
	if dragging && Input.is_action_pressed("left_click"): #If mouse dragging
		pass
	else: #When we release
		if not top_sprite == null:
			top_sprite.can_drag = false #Set can_drag to false
		top_sprite = null #Top sprite to null
		top_set = false
		dragging = false
	
	_print_status()

class SpritesSorter: #Custom sorter
	static func z_index(a, b): #Sort by z_index
		if a.z_index > b.z_index:
			return true
		return false

func _add_sprite(sprt): #Add sprite to list
	if not sprites.find(sprt) == -1: #If sprite exists
		return #Do nothing
	sprites.append(sprt) #Add sprite to list

func _remove_sprite(sprt): #Remove sprite from list
	if sprt == top_sprite and dragging: #This is for preventing the sprite dropping when we move the mouse fast
		 return
	sprt.can_drag = false #Set can_drag to false
	var idx = sprites.find(sprt) #find the index
	sprites.remove(idx) #remove

func _top_sprite(): #Get the top sprite
	if len(sprites) == 0: #If the list is empty
		return null
	sprites.sort_custom(SpritesSorter, "z_index") #Sort by z_index
	for i in sprites: #Set all can_drag to false
		i.can_drag = false
	return sprites[0] #Return top sprite

#Print status
func _print_status():
	var aux_sprt = []
	var aux_sprt_can_drag = []
	for i in sprites:
		aux_sprt.append(i.z_index)
	for i in sprites:
		aux_sprt_can_drag.append(i.can_drag)
	if not top_sprite == null:
		status.text = "Top: " + str(top_sprite.z_index) + " - Dragging: " + str(dragging)
		spriteList.text = "Sprites: " + str(aux_sprt) + " - Can drag: " + str(aux_sprt_can_drag)
	else:
		status.text = "Top: null - Dragging: " + str(dragging)
		spriteList.text = "Sprites: " + str(aux_sprt) + " - Can drag: " + str(aux_sprt_can_drag)