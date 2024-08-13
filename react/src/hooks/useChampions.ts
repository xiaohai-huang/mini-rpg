import { useState, useEffect, useMemo } from "react";
import {
  CHAMPION_CLASS_TO_CHINESE,
  ChampionClass,
  ChampionData,
  getChampions,
} from "src/api/hok";

const ALL_CLASSES = Object.entries(CHAMPION_CLASS_TO_CHINESE);

export default function useChampions() {
  const [champions, setChampions] = useState([] as ChampionData[]);
  const [loading, setLoading] = useState(true);
  const grouped = useMemo(() => {
    const data: Record<ChampionClass, ChampionData[]> = {
      ALL: champions,
      ASSASSIN: [],
      MAGE: [],
      MARKSMAN: [],
      SUPPORT: [],
      TANK: [],
      WARRIOR: [],
    };
    champions.forEach((champion) => {
      ALL_CLASSES.forEach(([key, value]) => {
        if (champion.classes.includes(value)) {
          data[key].push(champion);
        }
      });
    });
    return data;
  }, [champions]);

  useEffect(() => {
    setLoading(true);
    let mounted = true;
    getChampions()
      .then((data) => {
        mounted && setChampions(data);
      })
      .catch((e) =>
        console.error("error occurred while fetching champions data.", e)
      )
      .finally(() => mounted && setLoading(false));

    return () => {
      mounted = false;
    };
  }, []);

  return {
    loading,
    champions: grouped,
  };
}
