import type { ReactUnity, ScrollDirection } from "@reactunity/renderer";
import type { View } from "@reactunity/renderer/uitoolkit";
import classNames from "classnames";
import styles from "./index.module.scss";

type ScrollProps = {
  className?: string;
  children?: JSX.Element;
  style?: View<ReactUnity.UIToolkit.ScrollViewComponent>["style"];
  smoothness?: number;
  elasticity?: number;
  direction?: ScrollDirection;
  showVerticalScroller?: boolean;
  showHorizontalScroller?: boolean;
};

function Scroll({
  className,
  children,
  style,
  smoothness = 0.01,
  elasticity = 0.05,
  direction,
  showHorizontalScroller = false,
  showVerticalScroller = false,
}: ScrollProps) {
  return (
    <scroll
      className={classNames(className, styles.scroll)}
      smoothness={smoothness}
      elasticity={elasticity}
      style={
        typeof style === "string"
          ? style
          : { width: "100%", height: "100%", margin: 0, ...style }
      }
      direction={direction}
      // onAttachToPanel={(_, sender) => {
      //   // Hide the scroll bar
      //   const element = sender.Element;
      //   if (!showVerticalScroller) {
      //     element.verticalScrollerVisibility =
      //       Interop.UnityEngine.UIElements.ScrollerVisibility.Hidden;
      //   }
      //   if (!showHorizontalScroller) {
      //     element.horizontalScrollerVisibility =
      //       Interop.UnityEngine.UIElements.ScrollerVisibility.Hidden;
      //   }
      // }}
    >
      {children}
    </scroll>
  );
}

export default Scroll;
