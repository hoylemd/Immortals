# hutils.general module

# numeric validation function
def isNumeric(number):
	try:
		float(number)
		return True
	except ValueError:
		return False
		
# function to read in the contents of a file
def readFile(file_path):
	data_file = open(file_path, 'r')
	data = data_file.read()
	data_file.close()
	return data