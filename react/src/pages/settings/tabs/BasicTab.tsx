import { useState } from "react";
import classNames from "classnames";

import Select from "src/components/Select";
import styles from "./BasicTab.module.scss";

const PRODUCTION_URL = process.env.REACT_APP_PRODUCTION_URL;

type ScriptSource = "production" | "local";

function HandleRestart(type: ScriptSource, source?: string) {
  // Update source
  // switch (type) {
  //   case "local": {
  //     Renderer.Source.SourcePath = source;
  //     break;
  //   }
  //   case "production": {
  //     Renderer.Source.SourcePath = PRODUCTION_URL;
  //     break;
  //   }
  // }
  // Renderer.Invoke("Render", 0);
  console.log("restart");
}

function BasicTab() {
  const [scriptSourceType, setScriptSourceType] = useState<ScriptSource>(() =>
    Context.Source.SourcePath === PRODUCTION_URL ? "production" : "local"
  );
  const [localScriptSource, setLocalScriptSource] = useState(
    "http://192.168.31.224:3000/index.js"
  );

  return (
    <view className={styles.container}>
      <view className={classNames(styles.item, styles.scriptSource)}>
        <view className={styles.label}>Script Source</view>
        <view className={styles.content}>
          <view className={styles.select}>
            <Select
              value={scriptSourceType}
              onChange={(v) => setScriptSourceType(v)}
            >
              <Select.Option value="production">Production</Select.Option>
              <Select.Option value="local">Local Dev</Select.Option>
            </Select>
          </view>
          {scriptSourceType === "local" && (
            <input
              className={styles.input}
              value={localScriptSource}
              onChange={(e) => {
                setLocalScriptSource(e);
              }}
            />
          )}
        </view>
      </view>
      <view className={styles.bottom}>
        <view className={styles.sourcePath}>
          current source: {Context.Source.SourcePath}
        </view>
        <view className={styles.restart}>
          <button
            className={styles.btn}
            onClick={() => HandleRestart(scriptSourceType, localScriptSource)}
          >
            Restart
          </button>
        </view>
      </view>
    </view>
  );
}

export default BasicTab;
