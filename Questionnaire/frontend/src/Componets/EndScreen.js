import React, { useState, useContext, useEffect } from "react";
import "../App.css";
import { GameStateContext } from "../helpers/Contexts";
import { Questions } from "../helpers/Questions";
import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";

const EndScreen = () => {
  const { score, setScore, setGameState, userName } = useContext(
    GameStateContext
  );

  const [totalAttempts, setTotalAttempts] = useState(0);
  const navigate = useNavigate();

  const location = useLocation();
  const scoreFromLocation = location.state ? location.state.score : 0;
  const totalAttemptsFromLocation = location.state
    ? location.state.totalAttempts
    : 0;

  useEffect(() => {
    if (location.state && location.state.totalAttempts !== undefined) {
      setTotalAttempts(location.state.totalAttempts);
    }
  }, [location.state]);

  const restartQuiz = () => {
    setScore(0);
    setGameState("menu");
    navigate("/", { state: { totalAttempts: totalAttempts + 1 } }); // Update totalAttempts in location state
  };

  const totalQuestions = Questions.length;

  let resultMessage;
  if (scoreFromLocation < 5) {
    resultMessage = "Poor";
  } else if (scoreFromLocation >= 5 && scoreFromLocation <= 8) {
    resultMessage = "Good";
  } else {
    resultMessage = "Best";
  }

  return (
    <div className="EndScreen">
      <h1>Quiz Finished</h1>
      <h3>Welcome, {userName}</h3>
      <p>Total Quiz Points: {totalQuestions}</p>
      <p>Total Questions: {totalQuestions}</p>
      <p>Earned Points: {scoreFromLocation}</p>
      <p>Quiz Result: {resultMessage}</p>
      <button onClick={restartQuiz}>Restart Quiz</button>
    </div>
  );
};

export default EndScreen;
