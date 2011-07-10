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
		
class CreatureSprite(pygame.sprite.Sprite):
	def __init__(self, image, gridPos, facing):
		# initialize base class
		pygame.sprite.Sprite.__init__(self)
		
		# make & rotate the surface
		self.image = pygame.transform.rotate(image, facing)
		
		# position it
		self.rect = self.image.get_rect()
		self.rect.topleft = ((gridPos[0]*75),(gridPos[1]*75))
		self.facing = facing

def generateBoard(length, width, tilePath):
	board = []
	image = pygame.image.load(tilePath).convert()
	
	for y in range(length):
		line = []
		for x in range(width):
			tile = Tile(image, [x,y])
			line.append(tile)
		board.append(line)
		
	return board
	
def generateImmortalSprites(spritePath):
	sprites = []
	image = pygame.image.load(spritePath).convert()
	image.set_colorkey([0,255,0])
	
	sprites.append(CreatureSprite(image, (4,9), 0))
	sprites.append(CreatureSprite(image, (5,0), 180))
	
	return sprites
			