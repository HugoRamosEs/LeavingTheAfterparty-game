<?php

include "class/config/autoloader.php";
spl_autoload_register("Autoloader::load");
spl_autoload_register("Autoloader::loadDataBase");

try {
    $conn = DataBase::getInstance('root');
    if ($conn) {
        echo '{"codigo": 200, "mensaje":"Conectado correctamente", "respuesta":""}';
    } else {
        echo '{"codigo": 401, "mensaje":"Error intentando conectar", "respuesta":""}';
    }
} catch (Exception $e) {
    echo '{"codigo": 400, "mensaje":"Error fatal, revisa el código", "respuesta":""}';
}

// include "footer.php";