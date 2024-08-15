import { useNavigate } from "src/components/MiniRouter";
import Editor from "src/components/JsEditor";
import useSafeArea from "src/hooks/useSafeArea";

function Page() {
  const navigate = useNavigate();
  return (
    <view style={{ "--my-color": "lightblue", marginLeft: "var(--safe-left)" }}>
      <button
        onClick={() => {
          navigate("/");
        }}
      >
        Go to /
      </button>
      Ranking page
      <Editor />
      <Safe />
    </view>
  );
}
function Safe() {
  const area = useSafeArea();

  return (
    <view style={{ background: "lightblue" }}>
      <text>{`<b>useSafeArea</b>`}</text>
      <text>{`${JSON.stringify(area, null, 2)}`}</text>
    </view>
  );
}

export default Page;
