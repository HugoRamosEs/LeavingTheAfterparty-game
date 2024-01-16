<?php
class Config {
    
    public $sgbd;
    public $server;
    public $user;
    public $password;
    public $base;
    
    private static $_instance;
    
    private function __construct() {
        include 'users/root.php';
        $this->sgbd = $sgbd;
        $this->server = $server;
        $this->user = $user;
        $this->password = $password;
        $this->base = $base;
    }
    
    public static function getInstance() {
        if (!(self::$_instance instanceof self)) {
            self::$_instance = new self();
        }
        return self::$_instance;
    }
    
    public function clone() {}
    
}