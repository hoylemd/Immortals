# Immortals
# Primary game module
# basically tactical MTG
# by Michael D. Hoyle
# Last modified: 7/10/2011

# import libraries
import pygame
from hutils import colours
import ImmortalGraphics
import Creatures
import controlNodes
from controlNodes import ControlNode
import gameStates
from gameStates import currentState
import Immortals

# shorthand for blitting a sprite
def drawSprite(theSprite):
	if theSprite != None:
		screen.blit(theSprite.image, theSprite.rect)
# shorthand for drawing a line
DrawLine = pygame.draw.line
		
# initialize pygame
pygame.init() 

# Set up the window
size=[1050,750]
screen=pygame.display.set_mode(size)
pygame.display.set_caption("Immortals") 

# Used to manage how fast the screen updates
clock=pygame.time.Clock()

# generate the game terrain
terrain = ImmortalGraphics.generateBoard(10,10,"img\\tile.png")

# make the board grid
board = []
for i in range(10):
	line = []
	for j in range(10):
		line.append(None)
	board.append(line)

# generate the immortals
immortalSprites = ImmortalGraphics.generateImmortalSprites("img\\immortalSmall.png")
avatar = Creatures.Avatar(immortalSprites[0], 1,1,3,1,(9,4),25)
player = Immortals.Immortal(avatar)
avatar = Creatures.Avatar(immortalSprites[1], 1,1,3,1,(0,5),25)
opponent = Immortals.Immortal(avatar)

# insert their avatars into the board
board[player.avatar.position[0]][player.avatar.position[1]] = player.avatar
board[opponent.avatar.position[0]][opponent.avatar.position[1]] = opponent.avatar

# generate the gamestate
currentState = gameStates.GameState(player, opponent, board)

# set up the control tree
controlTree = ControlNode(0,0,size[0],size[1])
controlTree.addChild(ControlNode(0,0,750,750))
controlTree.addChild(ControlNode(750,0,300,750))

#Loop until the user clicks the close button.
quit = False

# Main Program Loop
while quit == False:
	
	# enable the control tree
	controlTree.enable()
	
	# Limit to 30 frames per second
	clock.tick(30)	 
	
	# Handle events
	for event in pygame.event.get():
		if event.type == pygame.QUIT: # exit
			quit=True
		if event.type == pygame.MOUSEBUTTONUP:
			controlTree.click(pygame.mouse.get_pos())
			
	# draw the background
	screen.fill(colours.grey)
	
	# draw the board
	for line in terrain:
		for tile in line:
			drawSprite(tile)
	# draw gridlines
	for x in range(11):
		DrawLine(screen, colours.black, [(x*75),0], [(x*75),750], 3)
		DrawLine(screen, colours.black, [0,(x*75)], [750,(x*75)], 3)
	
	# draw Immortal's avatars
	drawSprite(player.avatar.sprite)
	drawSprite(opponent.avatar.sprite)
	
	# update the screen.
	pygame.display.flip()

# Be IDLE friendly.
pygame.quit ()