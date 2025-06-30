import typescript from '@rollup/plugin-typescript'
import pkg from '../../package.json' with { type: 'json' }

const year = new Date().getFullYear()
const banner = `/*!
 * DigitalService.UI v${pkg.version} (${pkg.homepage})
 * Copyright 2014-${year} ${pkg.author}
 * Licensed under MIT (https://github.com/ColorlibHQ/adminlte/blob/master/LICENSE)
 */`

export default {
  input: 'src/ts/adminlte.ts',
  output: {
    file: 'dist/js/adminlte.js',
    format: 'umd',
    banner,
    name: 'DigitalService.UI'
  },
  plugins: [typescript()]
}
