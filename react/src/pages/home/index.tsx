import classNames from "classnames";
import { useNavigate } from "src/components/MiniRouter/index";

import styles from "./index.module.scss";
import Avatar from "src/components/Avatar";
import Scroll from "src/components/Scroll";
import Image from "src/components/Image";

import defaultAvatar from "../../assets/images/default-avatar.jpg";
import bgImage from "src/assets/images/backgrounds/bg-00.jpg";

// Entries
import BattleIcon from "src/assets/images/icons/battle.png";
import RankingIcon from "src/assets/images/icons/ranking.png";
import RankingBadgeIcon from "src/assets/images/icons/ranking-badge.png";
import RankingBadgeCircleIcon from "src/assets/images/icons/ranking-badge-number-circle.png";

// Top
import Settings from "src/assets/images/icons/settings-icon.png";
import MailIcon from "src/assets/images/icons/mail-icon.png";
import FriendsIcon from "src/assets/images/icons/friends-icon.png";
import GoldIcon from "src/assets/images/icons/gold-icon.png";
import DiamondIcon from "src/assets/images/icons/diamond-icon.png";
import VoucherIcon from "src/assets/images/icons/voucher-icon.png";

// Socials
import RightArrowIcon from "src/assets/images/icons/right-arrow-icon.png";
import PlaceholderAvatar from "src/assets/images/icons/default-avatar.png";

// Tabs
import TabsBackground from "src/assets/images/icons/bottom-bar.png";
import ChampionTabIcon from "src/assets/images/icons/champion-icon.png";
import CustomizeTabIcon from "src/assets/images/icons/customize-icon.png";
import PrepareTabIcon from "src/assets/images/icons/prepare-icon.png";
import TeamTabIcon from "src/assets/images/icons/team-icon.png";
import QuestTabIcon from "src/assets/images/icons/quest-icon.png";
import BackpackTabIcon from "src/assets/images/icons/backpack-icon.png";

function Page() {
  return (
    <view className={styles.container}>
      <Image className={styles.backgroundImage} src={bgImage} />

      <Top />

      <view style={{ marginTop: "auto" }}>
        <Entries />
        <view style={{ marginBottom: "25px" }}></view>
        <Tabs />
      </view>
      <view className={styles.socialContainer}>
        <Social />
      </view>
    </view>
  );
}

function SimpleIconButton({
  icon,
  onClick,
}: {
  icon: string;
  onClick?: () => void;
}) {
  return (
    <view className={styles.button} onClick={onClick}>
      <Image
        style={{
          width: "28px",
          height: "28px",
          objectFit: "contain",
          flexShrink: 0,
        }}
        src={icon}
      />
    </view>
  );
}

type BalanceProps = {
  className?: string;
  type: "gold" | "diamond" | "voucher";
  value: number;
};

const BALANCE_ICONS: Record<BalanceProps["type"], string> = {
  gold: GoldIcon,
  diamond: DiamondIcon,
  voucher: VoucherIcon,
};

function getIcon(type: BalanceProps["type"]) {
  return BALANCE_ICONS[type];
}

function Balance({ className = "", type, value }: BalanceProps) {
  return (
    <view className={classNames(styles.balance, className)}>
      <Image className={styles.image} src={getIcon(type)} />
      <view className={styles.value}>{value}</view>
    </view>
  );
}

function Top() {
  const user = {
    name: "巅峰小蛋",
    level: 30,
    avatar: defaultAvatar,
  };
  const navigate = useNavigate();
  return (
    <view className={styles.top}>
      <Avatar {...user} size={38} />
      <view className={styles.right}>
        <view className={styles.balances}>
          <Balance type="gold" value={1312} />
          <Balance type="diamond" value={4238} />
          <Balance type="voucher" value={251} />
        </view>

        <SimpleIconButton icon={FriendsIcon} />
        <SimpleIconButton icon={MailIcon} />
        <SimpleIconButton
          icon={Settings}
          onClick={() => navigate("/settings")}
        />
      </view>
    </view>
  );
}

function Social() {
  return (
    <view class={styles.social}>
      <view className={styles.title}>
        <text>排行</text>
      </view>
      <Scroll className={styles.scroll} direction="vertical">
        <view className={styles.list}>
          <Avatar avatar={PlaceholderAvatar} size={45} />
          <Avatar avatar={PlaceholderAvatar} size={45} />
          <Avatar avatar={PlaceholderAvatar} size={45} />
          <Avatar avatar={PlaceholderAvatar} size={45} />
          <Avatar avatar={PlaceholderAvatar} size={45} />
          <Avatar avatar={PlaceholderAvatar} size={45} />
          <Avatar avatar={PlaceholderAvatar} size={45} />
        </view>
      </Scroll>
      <view className={styles.expand}>
        <Image className={styles.icon} src={RightArrowIcon} />
      </view>
    </view>
  );
}

function Entries() {
  const navigate = useNavigate();

  return (
    <view className={styles.entries}>
      <Image
        className={styles.button}
        src={BattleIcon}
        onClick={() => navigate("/champion-select")}
      />
      <Image
        className={styles.button}
        src={RankingIcon}
        onClick={() => navigate("/ranking")}
      />
      {/* Ranking Badge */}
      <RankingBadge num={15} onClick={() => navigate("/ranking")} />
    </view>
  );
}

type RankingBadgeProps = {
  num: number;
  onClick?: () => void;
};

function RankingBadge({ num, onClick = () => {} }: RankingBadgeProps) {
  return (
    <view className={styles.badge} onClick={onClick}>
      {/* Badge */}
      <Image className={styles.image} src={RankingBadgeIcon} />
      {/* Badge Number */}
      <view
        className={styles["number-circle"]}
        style={{ backgroundImage: `url(${RankingBadgeCircleIcon})` }}
      >
        <text className={styles.number}>{num}</text>
      </view>
    </view>
  );
}

type IconButtonProps = {
  icon: string;
  label: string;
  onClick?: () => void;
  hideBar?: boolean;
};

function IconButton({
  icon,
  label,
  hideBar = false,
  onClick = () => {},
}: IconButtonProps) {
  return (
    <view className={styles.tab} onClick={onClick}>
      <view className={styles.content}>
        <Image className={styles.icon} src={icon} />
        <text className={styles.label}>{label}</text>
      </view>
      {!hideBar && <view className={styles.verticalBar} />}
    </view>
  );
}

function Tabs() {
  const navigate = useNavigate();
  return (
    <view
      className={styles.tabs}
      style={{ backgroundImage: `url(${TabsBackground})` }}
    >
      <view className={styles.wrapper}>
        <IconButton icon={ChampionTabIcon} label="英雄" />
        <IconButton
          icon={CustomizeTabIcon}
          label="定制"
          onClick={() => navigate("/test")}
        />
        <IconButton icon={PrepareTabIcon} label="备战" />
        <IconButton icon={TeamTabIcon} label="战队" />
        <IconButton icon={QuestTabIcon} label="任务" />
        <IconButton icon={BackpackTabIcon} label="背包" hideBar />
      </view>
    </view>
  );
}

export default Page;
