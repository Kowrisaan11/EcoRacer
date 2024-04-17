import "./App.css";
import { useState } from "react";
import { GameStateContext } from "./helpers/Contexts";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

/**import Components*/
import Menu from './Componets/Menu';
import Quiz from './Componets/Quiz';
import EndScreen from './Componets/EndScreen';

function App() {
  const [gameState, setGameState] = useState("menu");
  const [userName, setUserName] = useState("");
  const [score, setScore] = useState(0);

  return (
    <div className="App">
      <Router>
        <h1 className="heading">Eco Racer Questionnaire</h1>
        <GameStateContext.Provider
          value={{
            gameState,
            setGameState,
            userName,
            setUserName,
            score,
            setScore,
          }}
        >
          <Routes>
            <Route path="/" element={<Menu />} />
            <Route path="/Quiz" element={<Quiz />} />
            <Route path="/EndScreen" element={<EndScreen />} />
          </Routes>
        </GameStateContext.Provider>
      </Router>
    </div>
  );
}

export default App;

