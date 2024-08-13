import { render } from "@reactunity/renderer";
import "./index.scss";
import { useState } from "react";

function App() {
  const [show, setShow] = useState(false);
  return (
    <scroll>
      <text>{`Go to <color=red>src/index.tsx</color> to edit this file`}</text>
      <button onClick={() => setShow((prev) => !prev)}>Click me</button>
      {show && <view>Nice to meet you guys!</view>}

      <button
        onClick={() => {
          Interop.UnityEngine.SceneManagement.SceneManager.LoadScene(
            "Playground"
          );
        }}
      >
        Start Game
      </button>
    </scroll>
  );
}

render(<App />);
