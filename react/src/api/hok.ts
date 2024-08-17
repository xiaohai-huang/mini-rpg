import herosData from "./data/heros.json";
import skinsData from "./data/skins.json";

export type ChampionData = {
  id: number;
  /**
   * examples: "姬小满"
   */
  name: string;
  /**
   * examples: "武道奇才"
   */
  alias: string;
  /**
   * examples: "geya"
   */
  pinyin: string;
  /**
   * examples: "三分之地", "逐鹿", "云中漠地"
   */
  region: string;
  /**
   * examples: "法师", "战士/刺客"
   */
  classes: string;
  img: string;
  skins: ChampionSkinData[];
};

export type ChampionClass =
  | "ALL"
  | "TANK"
  | "WARRIOR"
  | "MAGE"
  | "ASSASSIN"
  | "MARKSMAN"
  | "SUPPORT";

export const CHAMPION_CLASS_TO_CHINESE: { [key in ChampionClass]: string } = {
  ALL: "全部",
  TANK: "坦克",
  WARRIOR: "战士",
  MAGE: "法师",
  ASSASSIN: "刺客",
  MARKSMAN: "射手",
  SUPPORT: "辅助",
};

const IMAGE_PREFIX =
  "https://staging-supabase.zhengxing.asia/storage/v1/object/public/photos/hok";

export async function getChampions(): Promise<ChampionData[]> {
  const data = herosData;

  const skinData: { [id: string]: ChampionSkinData[] } = {};
  skinsData.forEach((item) => {
    const names =
      item.skin_name === undefined
        ? []
        : (item.skin_name.split("|") as string[]);
    const championId = item.ename;
    const skins: ChampionSkinData[] = names.map((name, i) => {
      const id = i + 1;
      return {
        id,
        name,
        smallImage: `image/skin/small/${championId}/${id}.jpg`,
        largeImage: `${IMAGE_PREFIX}/skin/large/${championId}/${id}.jpg`,
      };
    });
    skinData[championId] = skins;
  });

  return data.yzzyxs_4880.map((item) => {
    const id = Number(item.yzzyxi_2602);
    const champion: ChampionData = {
      id,
      name: item.yzzyxm_4588,
      alias: item.yzzyxc_4613,
      img: `image/skin/small/${id}/1.jpg`,
      region: item.yxqy_9100,
      pinyin: item.yxpy_7753,
      classes: item.yzzyxz_1918,
      skins: skinData[id],
    };

    return champion;
  });
}

export type ChampionSkinData = {
  id: number;
  name: string;
  smallImage: string;
  largeImage: string;
};
