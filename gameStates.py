# global game states
	
currentState = None
	
class GameState:
	selectedCreature = None
	def __init__(self, player, opponent, board):
		self.player = player
		self.opponent = opponent
		self.board = board
		self.selectedCreature = None
		
	def getAtFromBoard(self, position):
		return self.board[position[0]][position[1]]
		
	def moveMe(self, creature, position):
		self.board[creature.position[0]][creature.position[1]] = None
		self.board[position[0]][position[1]] = creature
		
	def selectCreature(self, creature):
		self.selectedCreature = creature
		
	def deselectCreature(self):
		self.selectedCreature = None