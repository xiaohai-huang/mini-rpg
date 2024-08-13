import React, { useEffect, useState } from "react";
import type { UIToolkitElements } from "@reactunity/renderer/uitoolkit";

type DelayProps = {
  className?: string;
  style?: UIToolkitElements["view"]["style"];
  delay?: number;
  children: React.ReactNode;
};

function Delay({ className, style, delay = 500, children }: DelayProps) {
  const [show, setShow] = useState(false);
  useEffect(() => {
    const timer = setTimeout(() => {
      setShow(true);
    }, delay);
    return () => {
      clearTimeout(timer);
    };
  }, [delay]);

  if (typeof style === "string") {
    style = {};
  }

  return (
    <view
      className={className}
      style={{ visibility: show ? "visible" : "hidden", ...style }}
    >
      {show && children}
    </view>
  );
}

export default Delay;
