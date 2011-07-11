import pygame
from hutils import colours
import gameStates
from gameStates import currentState

# sprite groups
allSprites = pygame.sprite.Group()
creatureGroup = pygame.sprite.Group()
tileGroup = pygame.sprite.Group()

selectedSprite = None

# shorthand for drawing a line
DrawLine = pygame.draw.line

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
	
	def moveTo(position):
		self.rect.topleft = ((position[0]*75),(position[1]*75))

class SelectionSprite(pygame.sprite.Sprite):
	visible = False
	
	def __init__(self, image):
		# initialize base class
		pygame.sprite.Sprite.__init__(self)
		gridPos = (0,0)
		
		# make the surface
		self.image = image
		
		# position it
		self.rect = self.image.get_rect()
		self.rect.topleft = ((gridPos[0]*75),(gridPos[1]*75))
		
		# hide it
		self.visible = False
	
	def selectCreature(self, creature):
		self.rect.topleft = ((creature.position[0]*75),(creature.position[1]*75))
		self.visible = True
		
	def selectPosition(self, position):
		self.rect.topleft = position
		self.visible = True
		
	def deselect(self):
		self.visible = False
		
	
def registerTile(tile):
	allSprites.add(tile)
	tileGroup.add(tile)

def registerCreatureSprite(sprite):
	allSprites.add(sprite)
	creatureGroup.add(sprite)

def registerSelectionSprite(sprite):
	allSprites.add(sprite)
	selectionGroup.add(sprite)
	
def generateBoard(length, width, tilePath):
	board = []
	image = pygame.image.load(tilePath).convert()
	
	for y in range(length):
		line = []
		for x in range(width):
			tile = Tile(image, [x,y])
			registerTile(tile)
			line.append(tile)
		board.append(line)
		
	return board
	
def generateImmortalSprites(spritePath):
	sprites = []
	image = pygame.image.load(spritePath).convert()
	image.set_colorkey(colours.green)
	
	avatar = CreatureSprite(image, (4,9), 0)
	sprites.append(avatar)
	registerCreatureSprite(avatar)
	avatar = CreatureSprite(image, (5,0), 180)
	sprites.append(avatar)
	registerCreatureSprite(avatar)
	
	return sprites

def generateSelectionSprite(spritePath):
	image = pygame.image.load(spritePath).convert()
	image.set_colorkey(colours.green)
	
	sprite = SelectionSprite(image)
	print type(selectedSprite)
	selectedSprite = sprite
	print type(selectedSprite)
	return sprite
	
def drawSprite(theSprite, surface):
	if theSprite != None:
		surface.blit(theSprite.image, theSprite.rect)
			
def draw(surface):
	# draw background
	surface.fill(colours.grey)
	# draw terrain
	tileGroup.draw(surface)
	# draw gridlines
	for x in range(11):
		DrawLine(surface, colours.black, [(x*75),0], [(x*75),750], 3)
		DrawLine(surface, colours.black, [0,(x*75)], [750,(x*75)], 3)
	# draw creatires
	creatureGroup.draw(surface)
	# draw selection reticle
	'''selected = None#gameStates.currentState.selectedCreature
	if selected != None:
		selectedSprite.selectCreature(selected)
		drawSprite(selectionSprite, surface)
	else:
		selectedSprite.deselect()'''