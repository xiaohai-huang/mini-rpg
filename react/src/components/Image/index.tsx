import { BaseCmpProps } from "node_modules/@reactunity/renderer/dist/src/models/base";

type ImageProps = {
  style?: BaseCmpProps["style"];
  className?: string;
  src: string;
  onLoad?: (event: { type: string }) => void;
  onClick?: () => void;
};

function Image(props: ImageProps) {
  return <image {...props} />;
}

export default Image;
