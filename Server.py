import socket
import thread

HOST_PC = ''
PORT_PC = 5006

HOST_CEL = ''
PORT_CEL = 5054

def new_string(string):
	n1 = []
	lista = list(string)
	n=0
	for i in lista:
		if ord(i)>31 and n>2:
			n1.append(i)
		n+=1
	string = ''.join(n1)
	return string

def conecta(HOST,PORT):
        tcp = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
        orig = (HOST,PORT)
        tcp.bind(orig)
        tcp.listen(1)
        return tcp

def con_pc(HOST,PORT):
        global TELEFONES
        tcp = conecta(HOST,PORT)
        while True:
                str=""
                TELEFONES = []
                con, cliente = tcp.accept()
                while True:
                        msg = con.recv(4096)
                        if not msg: break
                        str = new_string(msg)
                con.close()
                t = str.split(",")
                print str
                for i in range(0,len(t)-1):
                        TELEFONES.append(t[i])
                MENSAGEM = t[len(t)-1]
                print(TELEFONES)
                print MENSAGEM
                while(MENSAGEM!=""):
                        pass

def con_cel(HOST,PORT):
    global MENSAGEM
    MENSAGEM = ""
    tcp = conecta(HOST,PORT)
    while True:
        while MENSAGEM == "":
            pass
        print("Esperando por celular")
        con, cliente = tcp.accept()
        print("Conectado com o celular")
        for telefone in TELEFONES:
            con.send(telefone)
        con.send("end")
        con.send(MENSAGEM)
        #apaga a mensagem anterior
        MENSAGEM=""
        print("Mensagem e telefones enviados para o celular")
        con.close()

thread.start_new_thread(con_pc,tuple([HOST_PC,PORT_PC]))
#thread.start_new_thread(con_cel,tuple([HOST_CEL,PORT_CEL]))
print "Servidor iniciado"
i=""
while i!='s':
        pass
    #print("para desligar o servidor digite s")
    #i = str(input())
