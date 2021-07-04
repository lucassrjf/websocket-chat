import React, { useState, useEffect } from "react";
import { Container, Row, Col, Button } from 'react-bootstrap';

import Login from './login';
import Chat from './chat';

import { getSocket } from '../services/webSocketService';

function Home() {

  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [history, setHistory] = useState("Sala #general");
  const [clients, setClients] = useState([]);

  const handleIsLoggedIn = (value) => {
    setIsLoggedIn(value);
  }

  useEffect(() => {
    
    let socket = getSocket();

    socket.onmessage = e => {
      let responseJson = JSON.parse(e.data);

      setHistory(history + "\n" + responseJson.MessageText);
      setClients(responseJson.UsersInRoom);
    };
  }, [history]);

  return (
    <>
      {isLoggedIn ?
        (
          <Chat history={history} clients={clients} />
        ) :
        (
          <Login clients={clients} handleIsLoggedIn={handleIsLoggedIn} />
        )
      }
    </>
  );
}

export default Home;