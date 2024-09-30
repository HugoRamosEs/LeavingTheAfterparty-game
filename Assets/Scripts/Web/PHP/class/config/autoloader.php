<?php

class Autoloader {
    private const CARPETAS = ['config', 'controller', 'model', 'view'];

    public static function load($clase) {
        foreach (self::CARPETAS as $carpeta) {
            $filePath = "class" . DIRECTORY_SEPARATOR . $carpeta . DIRECTORY_SEPARATOR . strtolower($clase) . '.class.php';
            if (file_exists($filePath)) {
                include $filePath;
                return;
            }
        }
    }

    public static function loadDataBase($clase) {
        foreach (self::CARPETAS as $carpeta) {
            $filePath = "class" . DIRECTORY_SEPARATOR . $carpeta . DIRECTORY_SEPARATOR . $clase . '.php';
            if (file_exists($filePath)) {
                include $filePath;
                return;
            }
        }

        throw new Exception("No se ha encontrado la $clase en $filePath");
    }
}
