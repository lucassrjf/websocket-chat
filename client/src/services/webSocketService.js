const socket = new WebSocket("ws:/localhost:5000/server");

const getSocket = () => {
	return socket;
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
		"MessageTo": (messageTo === "0" ? "ALL" : messageTo),
		"IsPrivate": isPrivate,
		"MessageText": message
	}

	socket.send(JSON.stringify(data));
}

export { getSocket, login, logout, sendMessage };


