import { useState } from "react";
import classNames from "classnames";

import ChampionList from "./ChampionList";
import { CHAMPION_CLASS_TO_CHINESE, ChampionClass } from "src/api/hok";
import useChampions from "src/hooks/useChampions";
import type { Style } from "@reactunity/renderer";
import styles from "./ChampionSelect.module.scss";

type ChampionSelectProps = {
  className?: string;
  style?: Style;
  visible?: boolean;
  selectedId?: number;
  disabled?: boolean;
  onClick?: (id: number) => void;
  onClose?: () => void;
};

function ChampionSelect({
  className,
  style = {},
  visible = true,
  selectedId,
  disabled = false,
  onClick = () => {},
  onClose = () => {},
}: ChampionSelectProps) {
  const [tab, setTab] = useState<ChampionClass>("ALL");
  const { champions } = useChampions();
  return (
    <view
      style={{ ...style, visibility: visible ? "visible" : "hidden" }}
      className={classNames(className, styles.container)}
    >
      {/* Tabs */}
      <view className={styles.tabs}>
        {Object.entries(CHAMPION_CLASS_TO_CHINESE).map(([key, value]) => (
          <view
            key={key}
            className={classNames(styles.button, {
              [styles.selected]: tab === key,
            })}
            onClick={() => setTab(key as ChampionClass)}
          >
            {value}
          </view>
        ))}
      </view>
      {/* Tab Content */}
      <view className={styles.tabContent}>
        <ChampionList
          champions={champions[tab]}
          selectedId={selectedId}
          disabled={disabled}
          onClick={onClick}
        />
        <view className={styles.closeButtonWrapper}>
          <view
            className={styles.closeButton}
            onClick={() => {
              onClose();
            }}
          >
            <text style={{ textAlign: "center", verticalAlign: "middle" }}>
              {"<"}
            </text>
          </view>
        </view>
      </view>
    </view>
  );
}

export default ChampionSelect;
