import { ReactUnity } from "@reactunity/renderer";

type SceneManagerType = {
  LoadSceneAdditive: (sceneName: string, callback: () => void) => void;
};

export const SceneManager: SceneManagerType = Globals.SceneManager;

type BridgeType = {
  StartGame: (heroId: number, skinId: number) => void;
  Navigate: (newUrl: string) => void;
  Url: ReactUnity.Reactive.IReactive<string>;
  CreateRouter: (cb: any) => void;
  app: {
    Handle: (
      method: string,
      path: string,
      body: string,
      end: (message: string) => void
    ) => void;
  };
};

export const Bridge: BridgeType = Globals.Bridge;
