# hutils.numberLists module

import random
import math

# function to generate a list of random integers
def generateInts(num_numbers, lower, upper):
	# calculate the range
	list_range = upper - lower

	# seed the random number generator
	random.seed()

	#initialize the list
	new_list = list()

	# generate the random ints
	for i in range(num_numbers):
		newNumber = int(math.floor((random.random() * list_range) 
		 + lower))
		new_list.append(newNumber)
		
	return new_list