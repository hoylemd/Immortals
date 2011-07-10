#Immortals creatures

import pygame
import ImmortalGraphics

class Creature:
	def __init__(self, sprite, attack, defense, movement, range):
		# record the game data
		self.sprite = sprite
		self.attack = attack
		self.defense = defense
		self.movement = movement
		self.range = range
		
class Immortal(Creature):
	def __init__(self, sprite, attack, defense, movement, health):
		# initialize base class
		Creature.__init__(self, sprite, attack, defense, movement, 1)
		
		# record additional data
		self.health = health
		
		# set up spell list
		self.spells = []
		
		# set up creature List
		self.creatures = []