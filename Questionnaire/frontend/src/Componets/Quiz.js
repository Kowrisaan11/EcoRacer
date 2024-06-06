import React, { useState, useContext, useEffect } from "react";
import "../App.css";
import { Questions } from "../helpers/Questions";
import { Feedbacks } from "../helpers/Feedbacks";
import { GameStateContext } from "../helpers/Contexts";
import { useNavigate } from "react-router-dom";
import { useFetchQuestion } from "../hooks/FetchQuestion";
/**redux store import */
import { useSelector } from "react-redux";

function Quiz() {
  const { questions } = useSelector((state) => state);
  const [{ isLoading, apiData, serverError }] = useFetchQuestion();

  const [currentQuestion, setCurrentQuestion] = useState(0);
  const [optionChosen, setOptionChosen] = useState("");
  const [showFeedback, setShowFeedback] = useState(false);
  const [isAnswerCorrect, setIsAnswerCorrect] = useState(false);
  const [score, setScore] = useState(0); // Initialize score state with zero

  const { setGameState, userName } = useContext(GameStateContext); // Access userName from context
  const navigate = useNavigate(); // Initialize useNavigate hook

  const chooseOption = (option) => {
    setOptionChosen(option);
  };

  const nextQuestion = () => {
    if (optionChosen === "") {
      setIsAnswerCorrect(false);
    } else if (Questions[currentQuestion].answer === optionChosen) {
      setIsAnswerCorrect(true);
      setScore((prevScore) => prevScore + 1); // Increment score if answer is correct
    } else {
      setIsAnswerCorrect(false);
    }
    setShowFeedback(true);
  };

  const finishQuiz = () => {
    if (Questions[currentQuestion].answer === optionChosen) {
      setScore((prevScore) => prevScore + 1); // Increment score if the answer is correct
    }
    const user = {name:userName, score}
    console.log(user)
    fetch("http://localhost:8080/user/add",{
      method:"POST",
      headers: { "Content-Type": "application/json" },
      body:JSON.stringify(user)
    }).then(()=>{
      console.log("New User Added")
    })
    
    setGameState("finished");
    navigate("/EndScreen", { state: { score } }); // Pass score to EndScreen component
  };

  const handleNextQuestion = () => {
    setShowFeedback(false);
    setCurrentQuestion((prevQuestion) => prevQuestion + 1);
    setOptionChosen("");
  };

  if (isLoading) return <h3 className="text-light">isLoading</h3>;
  if (serverError)
    return <h3 className="text-light">{serverError || "Unknown Error"}</h3>;

  return (
    <div className="Quiz">
      <h1 style={{ fontSize: "1.5em" }}>Question {currentQuestion + 1}</h1>
      <h2>{Questions[currentQuestion].prompt}</h2>
      <div className="questions">
        <button onClick={() => chooseOption("optionA")}>
          A. {Questions[currentQuestion].optionA}
        </button>
        <button onClick={() => chooseOption("optionB")}>
          B. {Questions[currentQuestion].optionB}
        </button>
        <button onClick={() => chooseOption("optionC")}>
          C. {Questions[currentQuestion].optionC}
        </button>
        <button onClick={() => chooseOption("optionD")}>
          D. {Questions[currentQuestion].optionD}
        </button>
      </div>

      <p>
        <strong>Score: {score}</strong>
      </p>

      {showFeedback && (
        <div
          className="feedback"
          style={{ color: isAnswerCorrect ? "blue" : "red" }}
        >
          <p>
            {isAnswerCorrect
              ? "Correct!"
              : optionChosen === ""
              ? "Skipped"
              : "Incorrect!"}
          </p>
          {optionChosen !== "" && (
            <p>Correct Answer: {Questions[currentQuestion].answer}</p>
          )}
          <p>{Feedbacks[currentQuestion].generalFeedback}</p>
          {currentQuestion === Questions.length - 1 ? (
            <button onClick={finishQuiz} id="nextQuestion">
              Finish Quiz
            </button>
          ) : (
            <button onClick={handleNextQuestion} id="nextQuestion">
              Next Question
            </button>
          )}
        </div>
      )}

      {!showFeedback && (
        <button onClick={nextQuestion} id="nextQuestion">
          Check Answer
        </button>
      )}
    </div>
  );
}

export default Quiz;
