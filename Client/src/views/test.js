import React, { useEffect } from "react";
import { Container, Row, Button } from 'react-bootstrap';

import { getNewSocket } from '../services/webSocketService';

function Test() {

	var socketA = getNewSocket();
	var socketB = getNewSocket();
	var socketC = getNewSocket();

	// Sempre deixa os sockets preparados para receber uma mensagem
	useEffect(() => {

		socketA.onmessage = e => {
			let responseJson = JSON.parse(e.data);
			console.log("A recebeu mensagem: " + responseJson.MessageText);
		};

		socketB.onmessage = e => {
			let responseJson = JSON.parse(e.data);
			console.log("B recebeu mensagem: " + responseJson.MessageText);
		};

		socketC.onmessage = e => {
			let responseJson = JSON.parse(e.data);
			console.log("C recebeu mensagem: " + responseJson.MessageText);
		};

	}, [socketA, socketB, socketC]);

	const startTests = () => {
		console.log("START");

		// Os timeouts ajudam a visualizar as mensagens de forma mais gradual
		setTimeout(() => {
			let dataA = {
				"Action": "LOGIN",
				"UserName": "A",
				"MessageText": ""
			}
			socketA.onopen = () => {
				socketA.send(JSON.stringify(dataA));
			};
			socketA.onopen(dataA);
			console.log("A realizou Login");
		}, 1000);

		setTimeout(() => {
			let dataB = {
				"Action": "LOGIN",
				"UserName": "B",
				"MessageText": ""
			}
			socketB.onopen = () => {
				socketB.send(JSON.stringify(dataB));
			};
			socketB.onopen(dataB);
			console.log("B realizou Login");
		}, 1000);

		setTimeout(() => {
			let dataC = {
				"Action": "LOGIN",
				"UserName": "C",
				"MessageText": ""
			}
			socketC.onopen = () => {
				socketC.send(JSON.stringify(dataC));
			};
			socketC.onopen(dataC);
			console.log("C realizou Login");
		}, 1000);

		setTimeout(() => {

			let dataAMessage = {
				"Action": "MESSAGE",
				"MessageTo": "0",
				"IsPrivate": false,
				"MessageText": "Mensagem de A para todos"
			}
			socketA.send(JSON.stringify(dataAMessage));

			console.log("A enviou mensagem para todos");

		}, 1500);

		setTimeout(() => {

			let dataAMessage = {
				"Action": "MESSAGE",
				"MessageTo": "C",
				"IsPrivate": false,
				"MessageText": "Mensagem de B mencionando C onde todos vêem"
			}
			socketB.send(JSON.stringify(dataAMessage));

			console.log("B enviou mensagem mencionando C");

		}, 2000);

		setTimeout(() => {

			let dataAMessage = {
				"Action": "MESSAGE",
				"MessageTo": "A",
				"IsPrivate": true,
				"MessageText": "Mensagem privada de C para A"
			}
			socketC.send(JSON.stringify(dataAMessage));

			console.log("C enviou mensagem privada para A");

		}, 2500);
	}
	return (
		<>
			<Container>
				<Row>TESTES</Row>
				<Row><small>Visualize os testes no console do navegador</small></Row>
				<Row><Button onClick={startTests}>Iniciar</Button></Row>
			</Container>
		</>
	);
}

export default Test;
