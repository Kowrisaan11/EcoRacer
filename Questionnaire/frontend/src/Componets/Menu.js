import "../App.css";
import { useContext } from "react";
import { useState } from "react"; // Import useState for storing input value
import { useNavigate } from "react-router-dom"; // Import useNavigate for navigation
import { GameStateContext } from "../helpers/Contexts";

function Menu() {
  const navigate = useNavigate(); // Initialize useNavigate hook
  const { gameState, setGameState, setUserName } = useContext(
    GameStateContext
  );
  const [userNameInput, setUserNameInput] = useState(""); // State for input value

  const startQuiz = () => {
    setUserName(userNameInput); // Set the username
    setGameState("playing");
    navigate("/Quiz"); // Navigate to Quiz component
  };

  return (
    <div className="Menu">
      <ol style={{ textAlign: 'left' }}>
        <li>Respond to 10 questions sequentially.</li>
        <li>Achieve 10 points for each correct answer.</li>
        <li>Select only one option from the four provided for each question.</li>
        <li>You are not allow to modify your answers before the quiz concludes.</li>
        <li>The final results will be announced at the conclusion of the quiz.</li>
      </ol>

      <label htmlFor="name" className="bold-label">Enter Your Name:</label>
      <input
        type="text"
        placeholder="Ex. Harry Potter"
        value={userNameInput} // Bind input value to state
        onChange={(event) => {
          setUserNameInput(event.target.value);
        }}
      />
      <button onClick={startQuiz}>Start Quiz</button> {/* Use startQuiz function */}
    </div>
  );
}

export default Menu;
