import { useEffect, useRef } from "react";

export default function useEffectEvent(cb: (...args: any[]) => void) {
  const currentCallback = useRef((...args: any[]) => {});
  useEffect(() => {
    currentCallback.current = cb;
  }, [cb]);
  return (...args: any[]) => currentCallback.current(...args);
}
