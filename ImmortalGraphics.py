import pygame
from hutils import colours

class Tile(pygame.sprite.Sprite):
	def __init__(self, image, gridPos):
		# initialize base class
		pygame.sprite.Sprite.__init__(self)
		
		# make the surface
		self.image = image
		
		# position it
		self.rect = self.image.get_rect()
		self.rect.topleft = ((gridPos[0]*75),(gridPos[1]*75))

def generateBoard(length, width, tilePath):
	board = []
	image = pygame.image.load(tilePath)
	
	for y in range(length):
		line = []
		for x in range(width):
			tile = Tile(image, [x,y])
			line.append(tile)
		board.append(line)
		
	return board
			