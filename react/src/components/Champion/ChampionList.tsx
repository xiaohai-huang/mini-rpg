import { useEffect, useRef } from "react";
import classNames from "classnames";
import type { ReactUnity } from "@reactunity/renderer";

import { ChampionData } from "src/api/hok";
import Scroll from "../Scroll";
import ChampionIcon from "./ChampionIcon";
import styles from "./ChampionList.module.scss";

type ChampionListProps = {
  className?: string;
  champions: ChampionData[];
  selectedId?: number;
  disabled?: boolean;
  onClick?: (id: number) => void;
};

const DEFAULT_SMOOTHNESS = 0.05;

function ChampionList({
  className = "",
  champions,
  selectedId,
  disabled,
  onClick = () => {},
}: ChampionListProps) {
  // const scrollRef = useRef<ReactUnity.UIToolkit.ScrollViewComponent>(null);
  // // Scroll to the top & Disable smoothness temporarily
  // useEffect(() => {
  //   if (scrollRef.current) {
  //     scrollRef.current.Element.scrollDecelerationRate = 0;
  //     scrollRef.current.ScrollTop = 0;
  //   }
  //   const timer = setTimeout(() => {
  //     if (scrollRef.current) {
  //       scrollRef.current.Element.scrollDecelerationRate = DEFAULT_SMOOTHNESS;
  //     }
  //   }, 0);

  //   return () => clearTimeout(timer);
  // }, [champions.length]);

  return (
    <view className={classNames(styles.container, className)}>
      <Scroll direction="vertical">
        <view className={styles.list}>
          {champions.map((champion) => (
            <ChampionIcon
              key={champion.id}
              id={champion.id}
              name={champion.name}
              img={champion.img}
              size={80}
              selected={champion.id === selectedId}
              disabled={disabled && champion.id !== selectedId}
              onClick={(id) => {
                onClick(id);
              }}
            />
          ))}
        </view>
      </Scroll>
    </view>
  );
}

export default ChampionList;
