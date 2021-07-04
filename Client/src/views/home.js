import React, { useState, useEffect } from "react";
import { Button, Modal } from 'react-bootstrap';

import Login from './login';
import Chat from './chat';

import { getSocket } from '../services/webSocketService';

function Home() {

  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [history, setHistory] = useState("Sala #general");
  const [clients, setClients] = useState([]);
  const [showHelp, setShowHelp] = useState(false);

  // Este componente controla o fluxo da tela, portanto ele tem que manipular
  // a instância global do socket
  useEffect(() => {

    let socket = getSocket();

    socket.onmessage = e => {
      let responseJson = JSON.parse(e.data);

      setHistory(history + "\n" + responseJson.MessageText);
      setClients(responseJson.UsersInRoom);
    };
  }, [history]);

  const handleIsLoggedIn = (value) => {
    setIsLoggedIn(value);
  }

  const handleClose = () => setShowHelp(false);
  const handleShow = () => setShowHelp(true);

  return (
    <>
      <Button
        variant="outline-info"
        onClick={handleShow}
      >
        Ajuda
      </Button>
      {isLoggedIn ?
        (
          <Chat history={history} clients={clients} />
        ) :
        (
          <Login clients={clients} handleIsLoggedIn={handleIsLoggedIn} />
        )
      }

      <Modal show={showHelp} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Ajuda</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <b>Login</b>
          <br />
          <ul>
            <li>Campo 'username' é obrigatório</li>
            <li>Campo 'username' é único (não pode existir nos membros da sala)</li>
          </ul>
          <b>Chat</b>
          <br />
          <ul>
            <li>Por padrão o chat vem configurado para enviar mensagem para todos da sala</li>
            <li>Para mencionar um usuário da sala, selecione o 'username' ao lado do botão 'Enviar'</li>
            <li>Para enviar uma mensagem privada, selecione o 'username', ao lado do botão 'Enviar', e marque a opção 'Mensagem privada'</li>
            <li>Não é permitido o envio de mensagem privada para todos da sala ao mesmo tempo (Por isso o campo 'some')</li>
            <li>Para sair, clique no botão 'Sair'. Você será direcionado(a) de volta ao login</li>
          </ul>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="primary" onClick={handleClose}>
            Entendi
          </Button>
        </Modal.Footer>
      </Modal>

    </>
  );
}

export default Home;
