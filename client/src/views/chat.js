import React, { useState, useEffect } from "react";
import { Container, Row, Col, Button, Form } from 'react-bootstrap';

import { sendMessage } from '../services/webSocketService';

function Chat(props) {

	const [message, setMessage] = useState("");
	const [messageTo, setMessageTo] = useState("0");
	const [privateMessage, setPrivateMessage] = useState(false);
	const [privateMessageHidden, setPrivateMessageHidden] = useState(messageTo !== "0");
	const [history, setHistory] = useState();

	useEffect(() => {
		setHistory(props.history);
		console.log("CHAT")
	  }, [history]);

	const handleChangeMessageTo = (e) => {
		setMessageTo(e.target.value);
		setPrivateMessageHidden(messageTo !== "0");
	}

	const handleChangeMessage = (e) => {
		setMessage(e.target.value);
	}

	const handleEnterPressed = (e) => {

		if (e.key === 'Enter') {
			setMessage("");
		}
	}

	const handleChangeTextArea = (e) => {
		setHistory(e.target.value);
	}

	const handleClickSend = () => {
		sendMessage(message, messageTo, privateMessage);
		// setHistory(props.history)
		console.log("CHAT_CLICK: " + props.history)
	}

	return (
		<>
			<Container>
				<h3>CHAT</h3>

				<Form.Group controlId="formMessages"  >
					{/* <Form.Label>Example textarea</Form.Label> */}
					<Form.Control
						as="textarea"
						rows={20}
						value={props.history}
						onChange={handleChangeTextArea}
					/>
				</Form.Group>

				<Form.Row>
					<Col>
						<Form.Control
							placeholder="Mensagem"
							onKeyPress={handleEnterPressed}
							onChange={handleChangeMessage}
							value={message}
						/>
					</Col>
				</Form.Row>

				<Form.Row className="align-items-center">
					<Col xs="auto" className="my-1">
						<Form.Label className="mr-sm-2" htmlFor="inlineFormCustomSelect" srOnly>
							Preference
						</Form.Label>
						<Form.Control
							as="select"
							className="mr-sm-2"
							id="inlineFormCustomSelect"
							custom
							onChange={handleChangeMessageTo}
						>
							<option value="0">Todos</option>
							<option value="1">Teste</option>
						</Form.Control>
					</Col>

					<Button
						variant="primary"
						onClick={handleClickSend}
						type="button"
					>
						Enviar
					</Button>
					{privateMessageHidden ? null :
						(<Col xs="auto" className="my-1">
							<Form.Check
								type="checkbox"
								id="customControlAutosizing"
								label="Mensagem privada"
								custom
								onChange={(e) => setPrivateMessage(e.target.checked)}
							/>
						</Col>
						)
					}

				</Form.Row>

			</Container>
		</>
	);
}

export default Chat;
