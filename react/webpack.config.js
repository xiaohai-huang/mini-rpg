const { merge } = require("webpack-merge");
const ImageMinimizerPlugin = require("image-minimizer-webpack-plugin");

module.exports = (env, baseConfig) => {
  const config = merge(baseConfig, {
    output: {
      assetModuleFilename: "static/media/[name].[hash][ext]",
    },
    optimization: {
      minimize: env === "production",
      minimizer: [
        "...",
        new ImageMinimizerPlugin({
          minimizer: {
            implementation: ImageMinimizerPlugin.sharpMinify,
            options: {
              encodeOptions: {
                jpeg: {
                  quality: 80,
                },
              },
            },
          },
        }),
      ],
    },
  });

  return config;
};
