<?php

class FrontController extends Controller {
    const DEFAULT_ACTION="show";
    const DEFAULT_CONTROLLER="HomeController";
    
    public function dispatch() {
        
        $parametros = null;
        
        if ($_SERVER["REQUEST_METHOD"]=="GET" && count($_GET)==0) {
            $controller_name = self::DEFAULT_CONTROLLER;
            $action = self::DEFAULT_ACTION;
        } else {
            $url = array_keys($_GET)[0];
            $url = $this->sanitize($url);
            $url = trim($url,"/");
            $url = filter_var($url,FILTER_SANITIZE_URL);
            $url = explode("/", $url);
            if (isset($url[0])) {
                $controller_name = ucwords($url[0])."Controller";
                if (isset($url[1])) {
                    $action = $url[1];
                }
                if (count($url) > 2) {
                    for ($i=2; $i<count($url); $i++) {
                        $parametros[] = strtolower($url[$i]);
                    }
                }
            }
        }
        
        if (file_exists("classes/controller/".strtolower($controller_name).".class.php")) {
            $controller = new $controller_name();
            if (method_exists($controller,$action)) {
                $controller->$action($parametros);
            } else {
                throw new Exception("No existe la acción definida $action de $controller_name");
            }
        } else {
            throw new Exception("No existe el controlador pedido $controller_name");
        }
        
    }
}

