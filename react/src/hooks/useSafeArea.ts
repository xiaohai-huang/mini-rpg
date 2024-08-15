import { useEffect, useState } from "react";
import { type UnityEngine } from "@reactunity/renderer";

export default function useSafeArea() {
  const [safeArea, setSafeArea] = useState(() => getSafeArea());

  useEffect(() => {
    setSafeArea(getSafeArea());
    let oldOrientation: UnityEngine.ScreenOrientation = 0;
    const timer = setInterval(() => {
      const orientation = Interop.UnityEngine.Screen.orientation;
      if (oldOrientation !== orientation) {
        oldOrientation = orientation;
        setSafeArea(getSafeArea());
      }
    }, 1000);

    return () => clearInterval(timer);
  }, []);
  return safeArea;
}

function getSafeArea() {
  const screen = Interop.UnityEngine.Screen;
  const width = screen.width;
  const height = screen.height;
  const area = Interop.UnityEngine.Screen.safeArea;
  const output = {
    left: (area.xMin / width) * 100,
    right: (1 - area.xMax / width) * 100,
    top: (area.yMin / height) * 100,
    bottom: (1 - area.yMax / height) * 100,
  };
  return output;
}
