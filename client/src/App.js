import {
  BrowserRouter as Router,
  Switch,
  Route,
} from "react-router-dom";
import Home from './views/home';
import Login from './views/login';
import Test from './views/test';

import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {

  // let teste = () => {
  //   let socket = new WebSocket("ws:/localhost:5000/server");
  //   connect(socket);
  // }

  // function connect(socket) {

  //   let myJSON = {
  //     "UserName": "xablau",
  //     "Message": "Olha minha mensagem"
  //   }



  //   socket.onopen = function () {
  //     console.log("About to send data");
  //     socket.send(JSON.stringify(myJSON)); // I WANT TO SEND THIS MESSAGE TO THE SERVER!!!!!!
  //     console.log("Message sent!");
  //   };
  // }


  return (
    <Router>
      <div>
        <Switch>
          <Route exact path="/">
            <Home />
          </Route>
          <Route exact path="/login">
            <Login />
          </Route>
          <Route exact path="/test">
            <Test />
          </Route>
        </Switch>
      </div>
    </Router>
  );
}

export default App;
