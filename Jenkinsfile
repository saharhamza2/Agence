pipeline {
  agent any

  options {
    buildDiscarder(logRotator(numToKeepStr: '10'))   // garde 10 builds
    timeout(time: 60, unit: 'MINUTES')               // timeout pipeline
    timestamps()                                      // logs propres
  }

  triggers {
    // Poll SCM toutes les 5 minutes (auto-detection de changements git)
    pollSCM('H/5 * * * *')
  }

  parameters {
    string(name: 'BRANCH', defaultValue: 'main', description: 'Branche à construire')
  }

  stages {

    stage('Checkout') {
      steps {
        checkout([$class: 'GitSCM',
          branches: [[name: "*/${params.BRANCH}"]],
          userRemoteConfigs: [[url: 'https://github.com/saharhamza2/Agence.git']]
        ])
      }
    }

    // Détection du type de projet (Maven ou Node.js)
    stage('Detect & Install') {
      steps {
        script {
          if (fileExists('pom.xml')) {
            echo "Projet détecté : Maven"
          } else if (fileExists('package.json')) {
            echo "Projet détecté : Node.js"
            sh 'npm ci'
          } else {
            echo "Aucun build system détecté (ni Maven, ni Node.js)"
          }
        }
      }
    }

    stage('Build') {
      steps {
        script {
          if (fileExists('pom.xml')) {
            sh 'mvn clean package -DskipTests=false'
          } else if (fileExists('package.json')) {
            sh 'npm run build || echo "Pas de script build dans package.json"'
          } else {
            echo "Skip build"
          }
        }
      }
    }

    stage('Tests') {
      steps {
        script {
          if (fileExists('pom.xml')) {
            sh 'mvn test'
          } else if (fileExists('package.json')) {
            sh 'npm test || true'
          } else {
            echo "Pas de tests"
          }
        }
      }
      post {
        always {
          // Rapports tests Maven (JUnit)
          junit allowEmptyResults: true, testResults: '**/target/surefire-reports/*.xml'
        }
      }
    }

    stage('Archive Artifacts') {
      steps {
        script {
          if (fileExists('target')) {
            archiveArtifacts artifacts: 'target/*.jar', allowEmptyArchive: true
          } else if (fileExists('dist')) {
            archiveArtifacts artifacts: 'dist/**', allowEmptyArchive: true
          } else {
            echo "Aucun artefact à archiver"
          }
        }
      }
    }
  }

  post {
    success {
      echo "✔ BUILD SUCCESS : ${env.JOB_NAME} #${env.BUILD_NUMBER}"
    }
    failure {
      echo "❌ BUILD FAILED : ${env.JOB_NAME} #${env.BUILD_NUMBER}"
    }
    always {
      cleanWs()   // nettoie workspace pour éviter conflits
    }
  }
}
