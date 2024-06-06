const { defineConfig } = require("@vue/cli-service");
const AutoImport = require('unplugin-auto-import/webpack')
const Components = require('unplugin-vue-components/webpack')
const { ElementPlusResolver } = require('unplugin-vue-components/resolvers')

module.exports = defineConfig({
  transpileDependencies: true,
  lintOnSave: false,
  configureWebpack: {
    plugins: [
      AutoImport({
        resolvers: [ElementPlusResolver()],
      }),
      Components({
        resolvers: [ElementPlusResolver()],
      }),
    ],
    externals: {
      'AMap': 'AMap' // 高德地图配置
      }
  },
  devServer: {

    proxy: {
       '/api': {
         target: 'http://localhost:9000',
         changeOrigin: true,
         pathRewrite: { '^/api': '/api' },
       },
    },
  },
});

