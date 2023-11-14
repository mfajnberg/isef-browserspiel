import fs from 'fs-extra'
import path from 'path'

const sourceDirPath = 'dist'
const wwwrootDestinationDirPath = '../backend-solution/web-api/wwwroot'
const assetsDestinationDirPath = '../backend-solution/web-api/wwwroot/assets'

function moveFilesWithDynamicNames() {
  const assetsDir = fs.readdirSync(path.join(sourceDirPath, 'assets'))
  const jsFile = assetsDir.find((file) => file.startsWith('index') && file.endsWith('.js'))
  const cssFile = assetsDir.find((file) => file.startsWith('index') && file.endsWith('.css'))

  if (jsFile) {
    const assetsDestinationDir = fs.readdirSync(assetsDestinationDirPath)
    const jsFileOld = assetsDestinationDir.find((file) => file.startsWith('index') && file.endsWith('.js'))
    try {
      fs.unlinkSync(path.join(assetsDestinationDirPath, jsFileOld))
      console.log(`Deleted: ${jsFileOld}`)
    } catch (error) {
      console.warn(`Failed deleting file ${jsFileOld}: ${error.message} \n There likely was nothing to delete yet`)
    }
    fs.copySync(path.join(sourceDirPath, 'assets', jsFile), path.join(assetsDestinationDirPath, jsFile))
    console.log(`New JavaScript file ${jsFile} moved successfully!`)
  }

  if (cssFile) {
    const assetsDestinationDir = fs.readdirSync(assetsDestinationDirPath)
    const cssFileOld = assetsDestinationDir.find((file) => file.startsWith('index') && file.endsWith('.css'))
    try {
      fs.unlinkSync(path.join(assetsDestinationDirPath, cssFileOld))
      console.log(`Deleted: ${cssFileOld}`)
    } catch (error) {
      console.warn(`Failed deleting file ${cssFileOld}: ${error.message} \n There likely was nothing to delete yet`)
    }
    fs.copySync(path.join(sourceDirPath, 'assets', cssFile), path.join(assetsDestinationDirPath, cssFile))
    console.log(`New CSS file ${cssFile} moved successfully!`)
  }
}

fs.copySync(path.join(sourceDirPath, 'index.html'), path.join(wwwrootDestinationDirPath, 'index.html'))
console.log('index.html moved successfully!')

moveFilesWithDynamicNames()