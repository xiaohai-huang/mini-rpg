import { useState } from "react";
import classNames from "classnames";

import { useNavigate } from "src/components/MiniRouter";
import Image from "src/components/Image";
import BasicTab from "./tabs/BasicTab";
import GraphicsTab from "./tabs/GraphicsTab";
import ControlsTab from "./tabs/ControlsTab";

import BackIcon from "src/assets/images/icons/arrow-go-back-line.png";
import styles from "./index.module.scss";

const TABS_INFO = {
  basic: { label: "基础", component: BasicTab },
  graphics: { label: "画面", component: GraphicsTab },
  controls: { label: "操作", component: ControlsTab },
} as const;

type Tab = keyof typeof TABS_INFO;

function Page() {
  const navigate = useNavigate();
  const [activeTab, setActiveTab] = useState<Tab>("basic");
  const TabContent = TABS_INFO[activeTab].component;

  return (
    <view className={styles.container}>
      <view className={styles.tabs}>
        <button
          className={styles.backButton}
          onClick={() => {
            navigate("/");
          }}
        >
          <Image className={styles.icon} src={BackIcon} />
        </button>
        {Object.entries(TABS_INFO).map(([tab, info]) => (
          <view
            key={tab}
            className={classNames(styles.tab, {
              [styles.active]: activeTab === tab,
            })}
            onClick={() => {
              setActiveTab(tab as Tab);
            }}
          >
            {info.label}
          </view>
        ))}
      </view>
      <view className={styles.content}>
        <view className={styles.title}>设置</view>
        <view class={styles.main}>
          <TabContent />
        </view>
      </view>
    </view>
  );
}

export default Page;
