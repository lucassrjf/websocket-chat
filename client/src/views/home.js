import React, { useState, useEffect } from "react";
import { Container, Row, Col, Button } from 'react-bootstrap';

import Login from './login';
import Chat from './chat';

import { getSocket } from '../services/webSocketService';

// const socket = getNewSocket();

// const handleSocket = (newSocket) => {
//   socket = newSocket;
// }

// var history = "Bem vindo";

function Home() {

  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [history, setHistory] = useState("OLA");

  const handleIsLoggedIn = (value) => {
    setIsLoggedIn(value);
  }

  const handleChangeHistory = (data) => {
    // let newHistory = history + data;
    // setHistory(data);
  }

  useEffect(() => {
    
    let socket = getSocket();

    socket.onmessage = e => {
      // let newHistory = history + "\n" + e.data;
      // handleChangeHistory(newHistory);
      console.log("History: " + history)
      console.log("e.data: " + e.data)
      setHistory(history + "\n" + e.data);
    };
  }, [history]);

  return (
    <>
      {isLoggedIn ?
        (
          <Chat history={history} handleChangeHistory={handleChangeHistory} />
        ) :
        (
          <Login handleIsLoggedIn={handleIsLoggedIn} handleChangeHistory={handleChangeHistory} />
        )
      }


      {/* <Container>
        <Row>O que deseja fazer?</Row>
        <Row>
          <Col><Button variant="primary" onClick={() => handleClick('login')}>Entrar no chat</Button></Col>
          <Col><Button variant="secondary" onClick={() => handleClick('test')}>Testar</Button></Col>
        </Row>
      </Container> */}
    </>
  );
}

export default Home;
