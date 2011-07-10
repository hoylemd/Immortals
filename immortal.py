# Immortals
# Primary game module
# basically tactical MTG
# by Michael D. Hoyle
# Last modified: 7/10/2011

# import libraries
import pygame
from hutils import colours
import ImmortalGraphics
import ImmortalCreatures

# shorthand for blitting a sprite
def drawSprite(theSprite):
	if theSprite != None:
		screen.blit(theSprite.image, theSprite.rect)
# shorthand for drawing a line
DrawLine = pygame.draw.line
		
# initialize pygame
pygame.init() 

# Set up the window
size=[1080,750]
screen=pygame.display.set_mode(size)
pygame.display.set_caption("Immortals") 

# Used to manage how fast the screen updates
clock=pygame.time.Clock()

# generate the game board
board = ImmortalGraphics.generateBoard(10,10,"img\\tile.png")

# generate the immortals
immortalSprites = ImmortalGraphics.generateImmortalSprites("img\\immortalSmall.png")
immortals = []
immortals.append(ImmortalCreatures.Immortal(immortalSprites[0], 1,1,3,25))
immortals.append(ImmortalCreatures.Immortal(immortalSprites[1], 1,1,3,25))

#Loop until the user clicks the close button.
quit = False

# Main Program Loop
while quit == False:
	
	# Limit to 30 frames per second
	clock.tick(30)	 
	
	# Handle events
	for event in pygame.event.get():
		if event.type == pygame.QUIT: # exit
			quit=True
			
	# draw the background
	screen.fill(colours.grey)
	
	# draw the board
	for line in board:
		for tile in line:
			drawSprite(tile)
	# draw gridlines
	for x in range(11):
		DrawLine(screen, colours.black, [(x*75),0], [(x*75),750], 3)
		DrawLine(screen, colours.black, [0,(x*75)], [750,(x*75)], 3)
	
	# draw Immortals
	
	for immortal in immortals:
		drawSprite(immortal.sprite)
	
	# update the screen.
	pygame.display.flip()

# Be IDLE friendly.
pygame.quit ()