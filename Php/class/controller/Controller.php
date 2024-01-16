<?php

class Controller {
    
    public function __construct() {}
    
    public static function sanitize($dades) {
        $dades = trim($dades);
        $dades = stripslashes($dades);
        $dades = htmlspecialchars($dades);
        return $dades;
    }
    
}