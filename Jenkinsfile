pipeline {
  agent any
  stages {
    stage('Restore and Build') {
      steps {
        withDotNet(sdk: '5.0.302', specificSdkVersion: true) {
          dotnetRestore(runtime: 'linux-x64')
          dotnetBuild(framework: 'net5.0', runtime: 'linux-x64', configuration: 'Release')
          dotnetPack(configuration: 'Release', runtime: 'linux-x64')
        }

      }
    }

    stage('Run, Publish, Archive') {
      agent any
      steps {
        withDotNet(sdk: '5.0.302', specificSdkVersion: true) {
          dotnetTest(framework: 'net5.0', runtime: 'net5.0')
          archiveArtifacts 'Bookmarks/bin/*'
          archiveArtifacts 'XBEL/bin/*'
          dotnetClean(framework: 'net5.0', configuration: 'Release', runtime: 'linux-x64')
        }

        cleanWs(cleanWhenAborted: true, cleanWhenFailure: true, cleanWhenNotBuilt: true, cleanWhenSuccess: true, cleanWhenUnstable: true, cleanupMatrixParent: true, deleteDirs: true, disableDeferredWipeout: true)
      }
    }

  }
}