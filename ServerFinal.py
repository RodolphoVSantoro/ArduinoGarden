import os

HOST = ''
PORT = 5004

#codifica para unicode
def str2unicode(string):
	n1 = []
	lista = list(string)
	for i in lista:
		if ord(i)>31:
			n1.append(i)
	string = ''.join(n1)
	return string

arquivo_telefones = open("telefones.txt", "r")
#tupla com todas as linhas do arquivo
telefones = arquivo_telefones.readlines()
arquivo_telefones.close()
for telefone in telefones:
	telefone = telefone.replace("\n","")



os.system("PAUSE")