<?php

// error_reporting(E_ALL);
// ini_set("display_errors", 1);

include "class/config/autoloader.php";
spl_autoload_register("Autoloader::load");
spl_autoload_register("Autoloader::loadDataBase");

try {
    if (DataBase::getInstance('consulta')) {
        $uController = new UserController();

        $datos = array(
            'email' => '',
            'escena' => '',
            'posX' => '',
            'posY' => '',
            'posZ' => '',
            'currentHp' => '',
            'currentStamina' => '',
            'inventory' => '',
            'orderInLayer' => '',
            'sotanoPasado' => '',
            'congeladorPasado' => '',
            'playaPasada' => '',
            'barcoBossPasado' => '',
            'ciudadBossPasado' => ''
        );

        foreach ($datos as $key => $value) {
            if (isset($_POST[$key])) {
                $datos[$key] = Controller::sanitize($_POST[$key]);
            }
        }

        $uController->guardarPartida($datos);

    } else {
        echo '{"codigo": 401, "mensaje":"Error intentando conectar", "respuesta":{}}';
    }

} catch (Exception $e) {
    echo '{"codigo": 400, "mensaje":"Error fatal", "respuesta":{"error":"' . $e->getMessage() . '"}}';
}