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
      steps {
        dotnetTest()
        dotnetPublish()
        archiveArtifacts './bin/*'
        dotnetClean()
      }
    }

  }
}