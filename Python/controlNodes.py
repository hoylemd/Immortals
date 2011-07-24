# controlNode

import pygame
from gameStates import currentState

class ControlNode(pygame.Rect):
	def __init__(self, top, left, width, height):
		# initialize base class
		pygame.Rect.__init__(self, top, left, width, height)
		
		# initialize children list
		self.children = []
		
		# enable it
		self.enabled = True
		
	def addChild(self, child):
		if self.contains(child):
			self.children.append(child)
		else:
			print "attempt to add uncontained control node"
			
	def click(self, mousePos):
		# check if a child was clicked
		for child in self.children:
			if child.collidepoint(mousePos) and child.enabled:
				return child.click(mousePos)
				
		# if none clicked, run handler
		if (self.enabled):
			return self.handler()
		
	def handler(self):
		print "click event"
		
	def enable(self):
		self.enabled = True
		
	def disable(self):
		self.enabled = False
		
class gridControl(ControlNode):
	def __init__(self, gridPos):
		# initialize base class
		controlNode.__init__(self, gridPos[0]*75, gridPos[1]*75,75,75)
		
		# store the grid position
		self.position = gridPos
		
	def handler(self):
		thingAt = currentState.getAtFromBoard(self.position)
		if thingAt != None:
			if currentState.selectedCreature == thingAt:
				currentState.selectedCreature = None
			else:
				currentState.selectedCreature = thingAt
		else:
			thingAt.moveTo(self.position)
			