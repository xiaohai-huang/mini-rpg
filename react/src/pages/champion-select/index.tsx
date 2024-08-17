import { useState, useMemo, useEffect } from "react";
import classNames from "classnames";
import { useNavigate } from "src/components/MiniRouter/index";

import myImage1 from "src/assets/images/backgrounds/bg-01.jpg";
import ChampionIcon from "src/components/Champion/ChampionIcon";
import useChampions from "src/hooks/useChampions";
import { type ChampionData } from "src/api/hok";
import Scroll from "src/components/Scroll";
import ChampionSelect from "src/components/Champion/ChampionSelect";
import styles from "./index.module.scss";
import Delay from "src/components/Delay";
import useEffectEvent from "src/hooks/useEffectEvent";
import { Bridge, SceneManager } from "src/lib/unity-objects";

function Page() {
  const navigate = useNavigate();
  const [tab, setTab] = useState<"champion" | "skin">("champion");
  const { champions } = useChampions();
  const allChampions = champions["ALL"];
  const [selectedChampionId, setSelectedChampion] = useState(-1);
  const [selectedSkinId, setSelectedSkinId] = useState(1);
  const [confirmed, setConfirmed] = useState(false);
  const champion: ChampionData | undefined = useMemo(
    () => allChampions.find((champ) => champ.id === selectedChampionId),
    [allChampions, selectedChampionId]
  );
  const championBackgroundImage = useMemo(
    () =>
      champion?.skins.find((skin) => skin.id === selectedSkinId)?.largeImage ??
      "",
    [champion, selectedSkinId]
  );

  const [showChampionsPanel, setShowChampionsPanel] = useState(false);
  const [championsPanelCached, setChampionsPanelCached] = useState(false);
  const championsRows = useMemo(() => {
    return groupBySize(allChampions, 2);
  }, [allChampions]);

  useEffect(() => {
    if (showChampionsPanel) {
      setChampionsPanelCached(true);
    }
  }, [showChampionsPanel]);

  const showExpandButton = !showChampionsPanel && tab === "champion";
  const ExpandButton = showExpandButton && (
    <view
      className={styles.expandButton}
      onClick={() => {
        setShowChampionsPanel(true);
      }}
    >
      {">"}
    </view>
  );

  const Left = (
    <view className={styles.leftSection}>
      {/* 英雄|皮肤 Tabs */}
      <view className={styles.tabs}>
        <view
          className={classNames(styles.button, {
            [styles.selected]: tab === "champion",
          })}
          onClick={() => setTab("champion")}
        >
          英雄
        </view>
        <view
          className={classNames(styles.button, {
            [styles.selected]: tab === "skin",
          })}
          onClick={() => setTab("skin")}
        >
          皮肤
        </view>
      </view>
      <view className={styles.listContainer}>
        {/* Placeholder */}
        <view className={classNames(styles.list, styles["place-holder"])}>
          <view className={styles.row}>
            <ChampionIcon.Placeholder size={80} />
            <ChampionIcon.Placeholder size={80} />
          </view>
        </view>
        {/* Champions Tab */}
        <view
          className={classNames(styles.listWrapper, "champions")}
          style={{
            visibility:
              tab === "champion" && !showChampionsPanel ? "visible" : "hidden",
          }}
        >
          <Scroll direction="vertical">
            <view className={styles.list}>
              {championsRows.map((row, i) => (
                <view key={i} className={styles.row}>
                  {row.map((champion) => (
                    <ChampionIcon
                      key={champion.id}
                      id={champion.id}
                      name={champion.name}
                      img={champion.img}
                      size={80}
                      selected={champion.id === selectedChampionId}
                      disabled={confirmed && champion.id !== selectedChampionId}
                      onClick={(id) => {
                        if (confirmed) return;
                        setSelectedChampion(id);
                        setSelectedSkinId(1);
                      }}
                    />
                  ))}
                </view>
              ))}
            </view>
          </Scroll>
        </view>
        {/* Skins Tab */}
        <view
          className={classNames(styles.listWrapper, "skins")}
          style={{
            visibility: tab === "skin" ? "visible" : "hidden",
          }}
        >
          <Scroll direction="vertical">
            <view className={styles.list}>
              {groupBySize(champion?.skins ?? [], 2).map((row, rowIndex) => {
                return (
                  <view key={rowIndex} className={styles.row}>
                    {row.map((skin, colIndex) => (
                      <ChampionIcon
                        key={skin.id}
                        id={skin.id}
                        name={
                          rowIndex === 0 && colIndex === 0 ? "经典" : skin.name
                        }
                        img={skin.smallImage}
                        size={80}
                        selected={skin.id === selectedSkinId}
                        onClick={(id) => {
                          setSelectedSkinId(id);
                        }}
                      />
                    ))}
                  </view>
                );
              })}
            </view>
          </Scroll>
        </view>

        {/* Expand Button Position*/}
        <view
          style={{
            position: "absolute",
            right: 0,
            top: "50%",
            transform: "translate(100%, -50%)",
          }}
        >
          {ExpandButton}
        </view>
      </view>
    </view>
  );

  const Middle = (
    <view className={styles.previewSection}>
      <view className={styles.container}>
        {selectedChampionId === -1 ? (
          <view className={styles.pickChampionText}>
            <view>请选择您的出战英雄</view>
          </view>
        ) : (
          <>
            <text style={{ marginTop: "var(--top-margin)", color: "white" }}>
              {champion.name}
            </text>
          </>
        )}
      </view>
    </view>
  );

  const Right = (
    <view className={styles.rightSection}>
      <button
        style={{
          margin: 0,
          backgroundColor: "lightblue",
        }}
        onClick={() => {
          setSelectedChampion(-1);
          setSelectedSkinId(1);
          setShowChampionsPanel(false);
          navigate("/");
        }}
      >
        取消
      </button>
      <view className={styles.selectedList}>
        {selectedChampionId !== -1 && (
          <ChampionIcon
            id={champion.id}
            img={champion.img}
            name={champion.name}
            size={64}
            selected
            showBorder={false}
          />
        )}
      </view>
      <button
        className={styles.confirm}
        style={{
          padding: "0.5rem",
        }}
        disabled={selectedChampionId === -1 || confirmed}
        onClick={() => {
          console.log("click confirm");
          setTab("skin");
          setShowChampionsPanel(false);
          setConfirmed(true);
        }}
      >
        确定
      </button>
    </view>
  );

  const ChampionLargeSelect = championsPanelCached && (
    <view className={styles.championLargeSelectWrapper}>
      <ChampionSelect
        className={styles.select}
        visible={showChampionsPanel}
        selectedId={selectedChampionId}
        disabled={confirmed}
        onClick={(id) => {
          if (confirmed) return;
          setSelectedChampion(id);
          setSelectedSkinId(1);
        }}
        onClose={() => setShowChampionsPanel(false)}
      />
    </view>
  );

  return (
    <Delay delay={0} className={styles.page}>
      <image
        style={{
          objectFit: "cover",
          position: "absolute",
          left: 0,
          top: 0,
          width: "100%",
          height: "100%",
        }}
        source={championBackgroundImage ? championBackgroundImage : myImage1}
      />
      {Left}
      {Middle}
      {Right}

      {ChampionLargeSelect}
      {confirmed && (
        <Countdown
          start={3}
          onFinish={() => {
            SceneManager.LoadSceneAdditive("Summoner's-Rift", () => {
              Bridge.StartGame(selectedChampionId, selectedSkinId);
            });
          }}
        />
      )}
    </Delay>
  );
}

function groupBySize<T>(array: T[], size: number): T[][] {
  return array.reduce((resultArray, item, index) => {
    const chunkIndex = Math.floor(index / size);
    if (!resultArray[chunkIndex]) {
      resultArray[chunkIndex] = [];
    }
    resultArray[chunkIndex].push(item);

    return resultArray;
  }, []);
}

function Countdown({ start = 10, onFinish = () => {} }) {
  const [num, setNum] = useState(start);
  const onFinishCallback = useEffectEvent(onFinish);
  useEffect(() => {
    const timer = setInterval(() => {
      setNum((prev) => {
        const newNum = prev - 1;
        if (newNum <= 0) {
          clearTimeout(timer);
        }
        return newNum;
      });
    }, 1000);

    return () => {
      clearInterval(timer);
    };
  }, []);

  useEffect(() => {
    if (num === 0) {
      onFinishCallback();
    }
    // eslint-disable-next-line
  }, [num]);

  return (
    <view
      style={{
        position: "absolute",
        width: "100%",
        height: "100%",
        display: "flex",
        alignItems: "center",
        pointerEvents: "none",
      }}
    >
      <text
        style={{
          backgroundColor: "rgba(0,0,0,0.3)",
          fontSize: "100px",
          width: "130px",
          height: "130px",
          textAlign: "center",
          verticalAlign: "middle",
        }}
      >
        {num.toString()}
      </text>
    </view>
  );
}

export default Page;
