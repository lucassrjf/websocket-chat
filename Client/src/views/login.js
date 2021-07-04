import React, { useState } from "react";
import { Container, Button, Form, Alert } from 'react-bootstrap';

import { login } from '../services/webSocketService';

function Login(props) {

	const [userName, setUserName] = useState('');

	const onChangeUsername = (e) => {
		props.setLoginError(false);
		setUserName(e.target.value)
	}

	const handleClickLogin = () => {

		if (!userName || userName === '' || props.clients.find(client => client === userName)) {
			props.setLoginError(true);
		} else {
			login(userName);
			// Para o usuário não ter tempo de ver a tela de chat em caso de erro
			setTimeout(() => {
				props.handleIsLoggedIn(true);
			}, 500);
		}
	}

	const handleEnterPressed = (e) => {
		if (e.key === 'Enter') {
			handleClickLogin();
		}
	}

	return (
		<>
			<Container>
				<h3>LOGIN</h3>
				<Form.Group controlId="formBasicEmail">
					<Form.Control
						type="text"
						placeholder="Forneça o username"
						value={userName}
						onKeyPress={(e) => handleEnterPressed(e)}
						onChange={(e) => onChangeUsername(e)} />
					<Form.Text className="text-muted">
						{props.loginError ? (
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
					type="button"
				>
					Entrar
				</Button>
			</Container>
		</>
	);
}

export default Login;
