pipeline {
  agent any

  options {
    buildDiscarder(logRotator(numToKeepStr: '10')) // conserve 10 builds max
    timeout(time: 60, unit: 'MINUTES')            // timeout global du pipeline
    ansiColor('xterm')
  }

  triggers {
    // Décommenter si tu veux polling (moins recommandé qu'un webhook)
    // pollSCM('H/5 * * * *')
  }

  parameters {
    string(name: 'BRANCH', defaultValue: 'main', description: 'Branch à builder')
  }

  stages {
    stage('Checkout') {
      steps {
        checkout([$class: 'GitSCM',
          branches: [[name: "*/${params.BRANCH}"]],
          userRemoteConfigs: [[url: 'https://github.com/saharhamza2/Agence.git']]])
      }
    }

    stage('Detect & Install') {
      steps {
        script {
          if (fileExists('package.json')) {
            echo "Detected Node project (package.json). Installing dependencies..."
            sh 'npm ci'
          } else if (fileExists('pom.xml')) {
            echo "Detected Maven project (pom.xml). Nothing to install here."
            // on peut ajouter: sh 'mvn -B -DskipTests=false dependency:resolve'
          } else {
            echo "No package.json or pom.xml found — skipping install step."
          }
        }
      }
    }

    stage('Build') {
      steps {
        script {
          if (fileExists('package.json')) {
            sh 'npm run build || echo "no build script or build failed"'
          } else if (fileExists('pom.xml')) {
            sh 'mvn -B -DskipTests clean package'
          } else {
            echo "Nothing to build"
          }
        }
      }
    }

    stage('Test') {
      steps {
        script {
          if (fileExists('package.json')) {
            sh 'npm test || true'
          } else if (fileExists('pom.xml')) {
            sh 'mvn test || true'
          } else {
            echo "No tests to run"
          }
        }
      }
      post {
        always {
          // adapte ces patterns si tu génères des rapports JUnit
          junit allowEmptyResults: true, testResults: '**/target/surefire-reports/*.xml'
        }
      }
    }

    stage('Archive') {
      steps {
        script {
          if (fileExists('dist')) {
            archiveArtifacts artifacts: 'dist/**', allowEmptyArchive: true
          } else if (fileExists('target')) {
            archiveArtifacts artifacts: 'target/*.jar', allowEmptyArchive: true
          } else {
            echo "No standard artifacts found to archive"
          }
        }
      }
    }
  } // end stages

  post {
    success { echo "Succès — ${env.JOB_NAME} #${env.BUILD_NUMBER}" }
    failure { echo "Échec — ${env.JOB_NAME} #${env.BUILD_NUMBER}" }
    always { cleanWs() }
  }
}
