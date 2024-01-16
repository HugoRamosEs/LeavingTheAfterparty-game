<?php

class User {
    
    public $email;
    public $usuario;
    public $contrasenya;
    
    public function __construct($email, $usuario, $contrasenya) {
        $this->email = $email;
        $this->usuario = $usuario;
        $this->contrasenya = $contrasenya;
    }
    
    /**
     * @return mixed
     */
    public function getEmail()
    {
        return $this->email;
    }

    /**
     * @return mixed
     */
    public function getUsuario()
    {
        return $this->usuario;
    }

    /**
     * @return mixed
     */
    public function getContrasenya()
    {
        return $this->contrasenya;
    }

    /**
     * @param mixed $email
     */
    public function setEmail($email)
    {
        $this->email = $email;
    }

    /**
     * @param mixed $usuario
     */
    public function setUsuario($usuario)
    {
        $this->usuario = $usuario;
    }

    /**
     * @param mixed $contrasenya
     */
    public function setContrasenya($contrasenya)
    {
        $this->contrasenya = $contrasenya;
    }
        
}

