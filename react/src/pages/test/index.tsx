import { useRef, useState } from "react";
import { useNavigate } from "src/components/MiniRouter";
import Image from "src/components/Image";

import styles from "./index.module.scss";
import type { ReactUnity } from "@reactunity/renderer";
import GradientTexture from "src/assets/images/backgrounds/gradient-texture.png";

function Page() {
  const navigate = useNavigate();
  const [type, setType] = useState("contain");
  const inputRef = useRef(null);
  const [show, setShow] = useState(false);
  return (
    <view
      className={styles.container}
      style={{
        width: "100%",
        height: "100%",
        backgroundImage: `url(${GradientTexture})`,
      }}
    >
      <button
        onClick={() => {
          navigate("/");
        }}
      >
        Go back to /
      </button>
      <input
        ref={inputRef}
        onBlur={() => {
          setShow(false);
        }}
        onClick={() => setShow(true)}
      />
      <input
        onKeyDown={(e) => {
          console.log("keydown");
        }}
      />
      {show && (
        <view
          onClick={() => setShow(false)}
          style={{
            position: "absolute",
            width: "100%",
            height: "100%",
            backgroundColor: "rgba(0,0,0,0.5)",
          }}
        />
      )}
    </view>
  );
}

export default Page;
