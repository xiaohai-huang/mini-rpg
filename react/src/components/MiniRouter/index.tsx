import { useReactiveValue } from "@reactunity/renderer";
import React, { createContext, useContext } from "react";
import { Bridge } from "src/lib/unity-objects";

const RouterContext = createContext({
  url: "/",
  navigate: (url: string) => {},
});

type MiniRouterProps = {
  children: React.ReactNode;
};

function MiniRouter({ children }: MiniRouterProps) {
  const url = useReactiveValue(Bridge.Url);
  const navigate = (newUrl: string) => {
    Bridge.Navigate(newUrl);
  };

  return (
    <RouterContext.Provider value={{ url, navigate }}>
      {children}
    </RouterContext.Provider>
  );
}

export function useNavigate() {
  return useContext(RouterContext).navigate;
}

export function useLocation() {
  return useContext(RouterContext).url;
}

type RouteProps = {
  path: string;
  element: JSX.Element;
  keepAlive?: boolean;
};

export function Route({ path, element, keepAlive = false }: RouteProps) {
  const location = useLocation();
  const match = path === location;
  if (!match && !keepAlive) return null;
  return (
    <view
      className={`mini-router-route-${path}`}
      style={{
        display: match ? "flex" : "none",
        position: "absolute",
        left: 0,
        top: 0,
        width: "100%",
        height: "100%",
      }}
    >
      {element}
    </view>
  );
}

export default MiniRouter;
