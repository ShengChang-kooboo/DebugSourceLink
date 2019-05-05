const webpack = require('webpack');
var path = require('path');
//!!!!忍不住吐槽一番：安装Webpack V4时，报错，说‘extract-text-webpack-plugin’不支持Webpack V4，
//于是Webpack降级到V3.0.0，使用另一个库‘mini-css-extract-plugin’，结果不会用也不想花时间研究，
//于是Webpack升级到V3.12.0，继续发现还是报同样错误，把Webpack降级到V3.11.0，还是报错，
//幸好找到一位大神https://github.com/webpack-contrib/extract-text-webpack-plugin/issues/753，
//拉我出坑————在Webpack V3时使用extract-text-webpack-plugin@^3.0.2版本，真是艰难的旅途啊！
const ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = [
    {
        entry: {
            'bundle': './ClientApp/app.js',
        },

        output: {
            path: path.resolve('./wwwroot'),
            filename: '[name].js'
        },

        resolve: {
            extensions: ['.js', '.json']
        },

        module: {
            rules: [
                {
                    test: /\.js/, use: [{
                        loader: 'babel-loader'
                    }], exclude: /node_modules/
                },
                {
                    test: /\.css$/, use: ExtractTextPlugin.extract({
                        fallback: "style-loader",
                        use: "css-loader"
                    })
                },
                {
                    test: /\.flow/, use: [{
                        loader: 'ignore-loader'
                    }]
                }
            ]
        },

        plugins: [
            new ExtractTextPlugin('style.css', { allChunks: true })
        ]
    }
];