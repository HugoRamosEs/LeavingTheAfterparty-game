<?php

class Autoloader{
    private const CARPETAS = ['config', 'controller', 'model', 'view'];
    
    public static function load($clase){
        foreach (self::CARPETAS as $carpeta) {
            if (file_exists("class/$carpeta/".strtolower($clase).'.class.php')) {
                include "class/$carpeta/".strtolower($clase).'.class.php';
                return;
            }
        }
    }
    
    public static function loadDataBase($clase){
        foreach (self::CARPETAS as $carpeta) {
            if (file_exists("class/$carpeta/$clase.php")) {
                include "class/$carpeta/$clase.php";
                return;
            }
        }
        throw new Exception("No s'ha trobat la definicio de la classe $clase");
    }
    
}