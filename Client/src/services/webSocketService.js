// Instância global da aplicação
const socket = new WebSocket("ws:/localhost:5000/server");

const getSocket = () => {
	return socket;
}

// Utilizada para criar mais de uma instância na mesma aplicação
// Utilizada nos casos de teste
const getNewSocket = () => {
	return new WebSocket("ws:/localhost:5000/server");;
}

const login = (userName) => {

	let data = {
		"Action": "LOGIN",
		"UserName": userName,
		"MessageText": ""
	}

	socket.onopen = () => {
		socket.send(JSON.stringify(data));
	};

	socket.onopen(data);
}

const logout = () => {
	socket.close();
	window.location = "/";
}

const sendMessage = (message, messageTo, isPrivate) => {
	
	let data = {
		"Action": "MESSAGE",
		"MessageTo": messageTo,
		"IsPrivate": isPrivate,
		"MessageText": message
	}

	socket.send(JSON.stringify(data));
}

export { getSocket, getNewSocket, login, logout, sendMessage };


