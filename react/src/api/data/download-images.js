const fs = require("fs");
const client = require("https");
const path = require("path");

const IMAGE_FOLDER = path.resolve(__dirname, "../../../public/image/skin");

function ensureDirectoryExists(filepath) {
  return fs.promises.mkdir(path.dirname(filepath), { recursive: true });
}

function downloadImage(url, filepath) {
  return new Promise(async (resolve, reject) => {
    // Ensure the directory exists
    await ensureDirectoryExists(filepath);

    client.get(url, (res) => {
      if (res.statusCode === 200) {
        res
          .pipe(fs.createWriteStream(filepath))
          .on("error", reject)
          .once("close", () => resolve(filepath));
      } else {
        // Consume response data to free up memory
        res.resume();
        reject(
          new Error(`Request Failed With a Status Code: ${res.statusCode}`)
        );
      }
    });
  });
}

function saveTo(heroId, numSkins, targetFolder) {
  const small_skin_images = new Array(numSkins)
    .fill(0)
    .map(
      (_, i) =>
        `https://game.gtimg.cn/images/yxzj/img201606/heroimg/${heroId}/${heroId}-smallskin-${
          i + 1
        }.jpg`
    );

  const large_skin_images = new Array(numSkins)
    .fill(0)
    .map(
      (_, i) =>
        `https://game.gtimg.cn/images/yxzj/img201606/skin/hero-info/${heroId}/${heroId}-bigskin-${
          i + 1
        }.jpg`
    );

  small_skin_images.map((url, i) => {
    const savePath = path.resolve(
      targetFolder,
      "small",
      `${heroId}`,
      `${i + 1}.jpg`
    );
    return downloadImage(url, savePath);
  });

  large_skin_images.map((url, i) => {
    const savePath = path.resolve(
      targetFolder,
      "large",
      `${heroId}`,
      `${i + 1}.jpg`
    );
    return downloadImage(url, savePath);
  });
}

var skins = JSON.parse(fs.readFileSync("skins.json", "utf8")).slice(100);
skins.forEach((skin) => {
  const heroId = skin.ename;
  const numSkins = skin.skin_name.split("|").length;
  saveTo(heroId, numSkins, IMAGE_FOLDER);
});
// saveTo(106, 5, IMAGE_FOLDER);
