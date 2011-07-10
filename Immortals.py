#immortals

import Creatures

class Immortal:
	avatar = None
	creaturesList = []
	spellsList = []
	manaGlyphs = []

	def __init__(self, avatar):
		self.avatar = avatar
		avatar.takeControl(self)
		creaturesList = []
		spellsList = []
		manaGlyphs = []