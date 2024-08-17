import { ReactUnity } from "@reactunity/renderer";
import { BaseCmpProps } from "node_modules/@reactunity/renderer/dist/src/models/base";
import { useLayoutEffect, useRef } from "react";

type ImageProps = {
  style?: BaseCmpProps["style"];
  className?: string;
  src: string;
  onLoad?: (event: { type: string }) => void;
  onClick?: () => void;
  keepAspectRatio?: boolean;
};

function Image({ keepAspectRatio = true, ...rest }: ImageProps) {
  const ref = useRef<ReactUnity.UGUI.ImageComponent>();
  useLayoutEffect(() => {
    if (ref.current) {
      ref.current.Image.preserveAspect = keepAspectRatio;
    }
  }, [keepAspectRatio]);

  return <image ref={ref} {...rest} />;
}

export default Image;
