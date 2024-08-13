import { useState } from "react";

export default function Editor() {
  const [code, setCode] = useState("");
  const [output, setOutput] = useState("");

  const run = () => {
    const modifiedSourceCode = `
      const __results=[];
          
      try{
        (()=>{
          ${code}
        })();
      }catch(err){
        return {results: __results, err};
      }
        
      return {results: __results};`.replaceAll("console.log", "__results.push");
    console.log(`var output =  (()=>{
        ${modifiedSourceCode}
      })() `);
    const { results: outputLines, err }: Output =
      // eslint-disable-next-line
      new Function(modifiedSourceCode)();

    console.log(outputLines);
    if (err) {
      setOutput(formatOutput([...outputLines, err]));
    } else {
      setOutput(formatOutput(outputLines));
    }
  };

  return (
    <view>
      <input
        style={{ height: "100px" }}
        value={code}
        onChange={(v) => setCode(v)}
      />
      <button onClick={run} style={{ marginTop: "1rem" }}>
        run
      </button>
      <view style={{ backgroundColor: "white", margin: "2rem 0" }}>
        <text id="editor-output">{output}</text>
      </view>
    </view>
  );
}

const formatOutput = (outputLines: any[]) => {
  let result = "";
  outputLines.forEach((line, idx) => {
    let formattedLine = line;
    // @ts-ignore
    if (line !== window) {
      formattedLine = JSON.stringify(line);
    }
    result += `${idx + 1}:  ${formattedLine}\n`;
  });
  return result;
};

type Output = { results: string[]; err: Error | undefined };
