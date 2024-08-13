import { useEffect, useState } from "react";

function ScriptSource() {
  const [source, setSource] = useState("");
  useEffect(() => {
    setSource(Context.Source.SourcePath);
  }, []);
  return (
    <view>
      <text>{`<b>Source</b>: ${source}`}</text>
    </view>
  );
}

export default ScriptSource;
