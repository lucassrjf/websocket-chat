import logo from './logo.svg';
import './App.css';

function App() {

  let teste = () => {
    let socket = new WebSocket("ws:/localhost:5000/server");
    connect(socket);
  }

  function connect(socket) {
    socket.onopen = function () {
        console.log("About to send data");
        socket.send("Hello World"); // I WANT TO SEND THIS MESSAGE TO THE SERVER!!!!!!
        console.log("Message sent!");
    };
  }


  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <button onClick={teste}>OLA MUNDO</button>
      </header>
    </div>
  );
}

export default App;
