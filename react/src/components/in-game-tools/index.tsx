import { useState } from "react";
import classNames from "classnames";

import { agent } from "src/lib/game-router";

import styles from "./index.module.scss";

import MoveUpIcon from "./icons/move-up.png";
import ResetLevelIcon from "./icons/settings.png";
import TimerIcon from "./icons/timer.png";
import TrashIcon from "./icons/trash-2.png";
import HeartIcon from "./icons/heart-pulse.png";
import GoldIcon from "./icons/circle-dollar-sign.png";
import SquirrelIcon from "./icons/squirrel.png";
import DogIcon from "./icons/dog.png";
import CatIcon from "./icons/cat.png";
import SwitchIcon from "./icons/arrow-left-right.png";
import ToolsIcon from "./icons/wrench.png";

type InGameToolsProps = {
  className?: string;
};
export default function InGameTools({ className }: InGameToolsProps) {
  const [showTools, setShowTools] = useState(false);

  return (
    <div className={className}>
      <Button
        className={styles.open}
        icon={ToolsIcon}
        onClick={() => setShowTools(true)}
      />
      <div className={styles.container} style={{ opacity: showTools ? 1 : 0 }}>
        <div style={{ marginTop: "10px" }} />

        {/* 己方英雄 */}
        <SelfHeroUtilities />
        {/* 敌方英雄 */}
        <OpponentHeroUtilities />
        {/* 通用 */}
        <GeneralUtilities />
        <div style={{ marginBottom: "10px" }} />

        <Button
          label="X"
          className={styles.close}
          onClick={() => setShowTools(false)}
        />
      </div>
    </div>
  );
}

function SelfHeroUtilities() {
  const [cd, setCD] = useState(false);
  const [invincible, setInvincible] = useState(false);
  return (
    <div>
      <text className={styles.title} style={{ color: "#7396A8" }}>
        己方英雄
      </text>
      <div className={styles.buttons}>
        <Button
          label="升级"
          icon={MoveUpIcon}
          onClick={() => {
            agent.post("/increment-level", {
              id: "hero-player",
            });
          }}
        />
        <Button
          label="重置等级"
          icon={ResetLevelIcon}
          onClick={() => {
            agent.post("/reset-level", {
              id: "hero-player",
            });
          }}
        />
        <ToggleButton
          label="冷却"
          checked={cd}
          icon={TimerIcon}
          onChange={(newValue) => {
            agent.post("/set-zero-cooldown", {
              id: "hero-player",
              enabled: newValue,
            });

            setCD(newValue);
          }}
        />
        <ToggleButton
          label="无敌"
          icon={HeartIcon}
          checked={invincible}
          onChange={(newValue) => {
            agent.post("/set-invincible", {
              id: "hero-player",
              enabled: newValue,
            });
            setInvincible(newValue);
          }}
        />
        <Button label="+10000" icon={GoldIcon} />
        <ToggleButton label="风暴龙王buff" icon={SquirrelIcon} />
        <Button label="友方人偶" icon={DogIcon} />
        <Button label="更换英雄" icon={SwitchIcon} />
      </div>
    </div>
  );
}

function OpponentHeroUtilities() {
  return (
    <div>
      <text className={styles.title} style={{ color: "#8E6A84" }}>
        敌方英雄
      </text>
      <div className={styles.buttons}>
        <Button label="升级" icon={MoveUpIcon} />
        <Button label="重置等级" icon={ResetLevelIcon} />
        <ToggleButton label="冷却" icon={TimerIcon} />
        <ToggleButton label="无敌" icon={HeartIcon} />
        <Button label="+10000" icon={GoldIcon} />
        <ToggleButton label="风暴龙王buff" icon={SquirrelIcon} />
        <Button
          label="韩信人偶"
          icon={DogIcon}
          onClick={() => {
            agent.post("/spawn-puppet", { id: "puppet-01", team: "red" });
          }}
        />
        <Button
          label="庄周人偶"
          icon={CatIcon}
          onClick={() => {
            agent.post("/spawn-puppet", { id: "puppet-02", team: "red" });
          }}
        />
      </div>
    </div>
  );
}

function GeneralUtilities() {
  const [waveEnabled, setWaveEnabled] = useState(false);
  return (
    <div>
      <text className={styles.title} style={{ color: "#7CA482" }}>
        通用
      </text>
      <div className={styles.buttons}>
        <ToggleButton
          label="兵线"
          checked={waveEnabled}
          onChange={(newValue) => {
            agent.post("/spawn-minion-wave", { enabled: newValue });

            setWaveEnabled(newValue);
          }}
        />
        <ToggleButton label="野怪" />
        <Button label="暴君" />
        <Button label="主宰" />
        <Button label="暗影暴君" />
        <Button label="暗影主宰" />
        <Button label="风暴龙王" />
        <Button
          label="清空人偶"
          icon={TrashIcon}
          onClick={() => {
            agent.post("/clear-puppets");
          }}
        />
      </div>
    </div>
  );
}

type ButtonProps = {
  className?: string;
  label?: string;
  icon?: string;
  onClick?: () => void;
};

function Button({ className, label, icon, onClick = () => {} }: ButtonProps) {
  return (
    <button className={classNames(styles.button, className)} onClick={onClick}>
      {icon && <image source={icon} className={styles.icon} />}
      {label ? <span className={styles.label}>{label}</span> : null}
    </button>
  );
}

type ToggleButtonProps = {
  label: string;
  icon?: string;
  checked?: boolean;
  onChange?: (checked: boolean) => void; // Add onChange prop
};

function ToggleButton({
  checked = false,
  label,
  icon,
  onChange = () => {}, // Default to an empty function
}: ToggleButtonProps) {
  const handleClick = () => {
    const newChecked = !checked; // Toggle checked state
    onChange(newChecked); // Call onChange with the new state
  };

  return (
    <button
      className={classNames(styles.button, { [styles.active]: checked })}
      onClick={handleClick} // Use handleClick here
    >
      {icon && <img src={icon} className={styles.icon} />}{" "}
      {/* Use <img> instead of <image> */}
      <span className={styles.label}>{label}</span>
    </button>
  );
}
