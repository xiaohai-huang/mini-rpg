import { render } from "@reactunity/renderer";

import useSafeArea from "src/hooks/useSafeArea";
import MiniRouter, { Route } from "./components/MiniRouter";
import Home from "./pages/home";
import ChampionSelect from "./pages/champion-select";
import Ranking from "./pages/ranking";
import TestPage from "./pages/test";
import SettingsPage from "./pages/settings";
import SummonersRiftPage from "./pages/summoners-rift";

import "./index.scss";

function App() {
  const area = useSafeArea();

  return (
    <view
      className="app-container"
      style={{
        "--safe-top": `${area.top}vh`,
        "--safe-bottom": `${area.bottom}vh`,
        "--safe-left": `${area.left}vw`,
        "--safe-right": `${area.right}vw`,
        "--top-margin": `${area.top + 0.5}vh`,
        "--bottom-margin": `${area.bottom + 0.5}vh`,
        "--left-margin": `${Math.max(5, area.left + 2)}vw`,
        "--right-margin": `${Math.max(5, area.right + 2)}vw`,
      }}
    >
      <MiniRouter>
        <Route path="/" keepAlive element={<Home />} />
        <Route path="/champion-select" element={<ChampionSelect />} />
        <Route path="/ranking" element={<Ranking />} />
        <Route path="/settings" element={<SettingsPage />} />
        <Route path="/test" element={<TestPage />} />
        <Route path="/summoners-rift" element={<SummonersRiftPage />} />
      </MiniRouter>
    </view>
  );
}

render(<App />);
