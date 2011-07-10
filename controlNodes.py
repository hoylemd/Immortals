# controlNode

import pygame
from globals import gameState

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
		print gameState
		
	def enable(self):
		self.enabled = True
		
	def disable(self):
		self.enabled = False