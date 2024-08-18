import { ReactUnity } from "@reactunity/renderer";
import { BaseCmpProps } from "node_modules/@reactunity/renderer/dist/src/models/base";
import { useLayoutEffect, useRef } from "react";

type ImageProps = {
  style?: BaseCmpProps["style"];
  className?: string;
  src: string;
  onLoad?: (event: { type: string }) => void;
  onClick?: () => void;
  preserveAspect?: boolean;
};

function Image({ preserveAspect = false, ...rest }: ImageProps) {
  const ref = useRef<ReactUnity.UGUI.ImageComponent>();
  useLayoutEffect(() => {
    if (ref.current) {
      ref.current.Image.preserveAspect = preserveAspect;
    }
  }, [preserveAspect]);

  return <image ref={ref} {...rest} />;
}

export default Image;
