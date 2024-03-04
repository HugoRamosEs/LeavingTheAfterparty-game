<?php

// error_reporting(E_ALL);
// ini_set("display_errors", 1);

include "class/config/autoloader.php";
spl_autoload_register("Autoloader::load");
spl_autoload_register("Autoloader::loadDataBase");

try {
    $conn = DataBase::getInstance('consulta');
    if ($conn) {
        echo '{"codigo": 200, "mensaje":"Conectado correctamente", "respuesta":""}';
        echo '<pre>';
        var_dump($conn);
    } else {
        echo '{"codigo": 400, "mensaje":"Error intentando conectar", "respuesta":""}';
    }

    
    // $usuarioPrueba = new User('hello@gmail.com', 'hello', '123');

    // $uController = new UserController();
    // $uModel = new UserModelo();

    // $uPruebaRegistrar = $uController->register($usuarioPrueba);
    // echo "<pre>";
    // var_dump($uPruebaRegistrar);
    // echo "</pre>";
    // $uPruebaLogin = $uController->login($usuarioPrueba);

} catch (Exception $e) {
    echo '{"codigo": 400, "mensaje":"Error fatal, revisa el código", "respuesta":""}';
}

