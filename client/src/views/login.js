import React, { useState } from "react";
import { Container, Button, Form, Alert } from 'react-bootstrap';
// const WebSocketService = require('../services/webSocketService');

import { login } from '../services/webSocketService';

function Login(props) {

	const [userName, setUserName] = useState('');
	const [loginError, setLoginError] = useState(false);

	const onChangeUsername = (e) => {
		setLoginError(false);
		setUserName(e.target.value)
	}

	const handleClickLogin = () => {
		console.log(props.clients)
		if (!userName || userName === '' || props.clients.find(client => client === userName)) {
			setLoginError(true);
		} else {
			login(userName);
			props.handleIsLoggedIn(true);
		}
	}

	return (
		<>
			<Container>
				<h3>LOGIN</h3>
				<Form>
					<Form.Group controlId="formBasicEmail">
						{/* <Form.Label>Login</Form.Label> */}
						<Form.Control
							type="text"
							placeholder="Forneça o username"
							value={userName}
							onChange={(e) => onChangeUsername(e)} />
						<Form.Text className="text-muted">
							{loginError ? (
								<Alert variant='danger'>
									O username informado é inválido ou já está em utilização em outro usuário
								</Alert>
							) : (
								<>Forneça um username para entrar</>
							)}
						</Form.Text>
					</Form.Group>
					<Button
						variant="primary"
						onClick={handleClickLogin}
					>
						Entrar
					</Button>

				</Form>

			</Container>
		</>
	);
}

export default Login;
