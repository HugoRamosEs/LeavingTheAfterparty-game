<?php

// error_reporting(E_ALL);
// ini_set("display_errors", 1);

header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: POST, GET, OPTIONS");
header("Access-Control-Allow-Headers: Content-Type");

include "class/config/autoloader.php";
spl_autoload_register("Autoloader::load");
spl_autoload_register("Autoloader::loadDataBase");

try {
    if (DataBase::getInstance('consulta')) {
        
        $userUnity = new User('', '', '');
        $uController = new UserController();
        $uModel = new UserModelo();

        $userUnity->setEmail(Controller::sanitize($_POST['user']));
        $userUnity->setContrasenya(Controller::sanitize($_POST['password']));

        $uPruebaLogin = $uController->login($userUnity);

    } else {
        echo '{"codigo": 401, "mensaje":"Error intentando conectar", "respuesta":""}';
    }

} catch (Exception $e) {
    echo '{"codigo": 400, "mensaje":"Error fatal", "respuesta":""}';
}