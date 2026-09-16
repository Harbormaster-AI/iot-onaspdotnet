resource "kubernetes_service" "app-master" {
    metadata {
        name = "app-master"
    }

    spec {
        selector = {
          app  = "iotonaspdotnet"
        }
        port {
            name        = "http"
            port        = 80
            target_port = #DefaultPort()
        }

        port {
            name        = "db-port"
            port        = 
            target_port = 
        }

        port {
            port        = #DefaultPort()
            target_port = #DefaultPort()
            name        = "app-port"
        }

        type = ""
    }
  
}
