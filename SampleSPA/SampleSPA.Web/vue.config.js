// vue.config.js

module.exports = {
  outputDir: './wwwroot/',
  filenameHashing: false,

  pluginOptions: {
    sourceDir: 'src'
  },

  configureWebpack: {

    performance: {
      hints: false
    },
    optimization: {
      splitChunks: false
    },

    output: {
      filename: 'app.js'
    },
    devtool: 'source-map',
    resolve: {
      alias: {
        '@': require('path').resolve(__dirname, 'src'),
        vue$: 'vue/dist/vue.runtime.esm.js'
      }
    }
  },

  productionSourceMap: false
}
