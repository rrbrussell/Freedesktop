pipeline {
  agent any
  stages {
    stage('Work on XBEL') {
      steps {
        dotnetRestore(runtime: 'linux-x64', project: 'XBEL.Test/XBEL.Test.csproj', showSdkInfo: true)
        dotnetBuild(framework: 'net5.0', runtime: 'linux-x64', configuration: 'Release', project: 'XBEL/XBEL.csproj')
        dotnetPack(configuration: 'Release', project: 'XBEL/XBEL.csproj')
        dotnetTest(project: 'XBEL.Test/XBEL.Test.csproj', resultsDirectory: 'test_results', configuration: 'Release', framework: 'net5.0', runtime: 'linux-x64')
        archiveArtifacts 'XBEL/bin/*'
        cleanWs(deleteDirs: true, cleanupMatrixParent: true, cleanWhenUnstable: true, cleanWhenSuccess: true, cleanWhenNotBuilt: true, cleanWhenFailure: true, cleanWhenAborted: true)
      }
    }

  }
}