import {
  BrowserRouter as Router,
  Switch,
  Route,
} from "react-router-dom";
import Home from './views/home';
import Test from './views/test';

import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <Router>
      <div>
        <Switch>
          <Route exact path="/">
            <Home />
          </Route>
          <Route exact path="/test">
            <Test />
          </Route>
          <Route exact path="/:error">
            <Home />
          </Route>
        </Switch>
      </div>
    </Router>
  );
}

export default App;
