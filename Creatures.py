#Immortals creatures

import pygame
import Graphics
import Immortals
import gameStates
from gameStates import currentState

class Creature:
	sprite = None
	attack = 0
	defense = 0
	movement = 0
	range = 0
	position = (0,0)
	owner = None
	def __init__(self, sprite, attack, defense, movement, range, initialPosition):
		# record the game data
		self.sprite = sprite
		self.attack = attack
		self.defense = defense
		self.movement = movement
		self.range = range
		self.position = initialPosition
		self.owner = None
		
	def moveTo(self, position):
		currentState.moveMe(self, position)
		self.sprite.moveTo(position)
		self.position = position
		
	def takeControl(self, immortal):
		self.owner = immortal
		
class Avatar(Creature):
	health = 0
	def __init__(self, sprite, attack, defense, movement,range, initialPosition, health):
		# initialize base class
		Creature.__init__(self, sprite, attack, defense, movement, range, initialPosition)
		
		# record additional data
		self.health = health