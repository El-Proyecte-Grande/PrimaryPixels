pipeline {

    agent any

    stages {

        stage("build") {

            steps {
                echo 'build stage'
            }
        }

        stage("test") {
            when {
                expression{
                    env.BRANCH_NAME == 'main' || env.BRANCH_NAME == 'f/jenkins'
                }
            }
            steps {
                echo 'test stage'
            }
        }

        stage("deploy") {

            steps {
                echo 'deploy stage'
            }
        }
    }
    post {
        always {
            echo 'this happens always'
        }
        success{
            echo 'horray'
        }
        failure{
            echo 'oh no'
        }
    }
}