<?php

class DataBase {
    
    public $link;
    private static $_instance;
    
    private function __construct($sgbd, $server, $user, $pwd, $base, $port) {
        switch ($sgbd) {
            case 'mysql':
                if ($link = new mysqli($server, $user, $pwd, $base, $port)) {
                    $this->link = $link;
                } else {
                    throw new Exception("No se ha podido conectar a la base de datos.");
                }
                break;
        }
    }
    
    public static function getInstance(String $tipoConexion, String $dbNombre = 'myweb', String $sgbdName = 'mysql', int $port = 12169) {
        
        if($tipoConexion === 'generic') {
            $sgbd = $sgbdName;
            $server = ini_get('mysqli.default_host');
            $user = ini_get('mysqli.default_user');
            $pwd = ini_get('mysqli.default_pw');
            $base = $dbNombre;
            $port = $port;
        } elseif ($tipoConexion === 'consulta') {
            $consulta = Config::getInstance();
            $sgbd = $consulta->sgbd;
            $server = $consulta->server;
            $user = $consulta->user;
            $pwd = $consulta->password;
            $base = $consulta->base;
            $port = $consulta->port;
        } elseif ($tipoConexion === 'root') {
            $root = Config::getInstance();
            $sgbd = $root->sgbd;
            $server = $root->server;
            $user = $root->user;
            $pwd = $root->password;
            $base = $root->base;
            $port = $root->port;
        } else {
            throw new Exception("No se ha podido crear la conexión a la base de datos.");
        }
        
        if (!(self::$_instance instanceof self)) {
            self::$_instance = new self($sgbd, $server, $user, $pwd, $base, $port);
        }
        return self::$_instance;
    }
    
    public function destroy() {
        $this->link->close();
    }
    
    public function executeSQL ($sSQL, $aParam = null) {
        if ($stmt = $this->link->prepare($sSQL)) {
            if ($stmt->execute($aParam)) {
                $res = $stmt->get_result();
                if ($res) {
                    $dades = $res->fetch_all();
                    return $dades;
                } else {
                    return true;
                }
            }
        }
        return false;
    }
    
    public function changeUser ($user, $pwd, $db) {
        $this->link->change_user($user, $pwd, $db);
    }
    
    private function clone() {}
    
}

