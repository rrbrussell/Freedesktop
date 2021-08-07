pipeline {
  agent any
  stages {
    stage('Restore and Build') {
      steps {
        dotnetRestore()
        dotnetBuild()
      }
    }

    stage('Run, Publish, Archive') {
      agent any
      steps {
        dotnetTest()
        dotnetPublish()
        archiveArtifacts '*/bin/*'
        dotnetClean()
      }
    }

  }
}