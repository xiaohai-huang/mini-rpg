import { ReactUnity } from "@reactunity/renderer";

type SceneManagerType = {
  LoadSceneAdditive: (sceneName: string, callback: () => void) => void;
};

export const SceneManager: SceneManagerType = Globals.SceneManager;

type BridgeType = {
  StartGame: (heroId: number, skinId: number) => void;
  Navigate: (newUrl: string) => void;
  Url: ReactUnity.Reactive.IReactive<string>;
};

export const Bridge: BridgeType = Globals.Bridge;