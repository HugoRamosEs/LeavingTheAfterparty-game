<?php

class UserModelo implements CRUDable
{
    public function read($obj = null) {
        $dbConsulta = DataBase::getInstance();
        $sql = "SELECT * FROM [user]";
        $consulta = $dbConsulta->executeSQL($sql);

        return $consulta;
    }

    public function create($obj) {
        $dbGeneric = DataBase::getInstance();
        $sql = "INSERT INTO [user] (email, username, password) VALUES (?, ?, ?)";
        $params = [$obj->email, $obj->usuario, $obj->contrasenya];
        $consulta = $dbGeneric->executeSQL($sql, $params);

        return $consulta;        
    }

    public function update($obj) {
        $dbGeneric = DataBase::getInstance();
        $sql = "UPDATE [user] SET status = ? WHERE email = ?";
        $status = 1;
        $params = [$status, $obj->email];
        $consulta = $dbGeneric->executeSQL($sql, $params);
        
        return $consulta;        
    }

    public function delete($obj) {
        $dbGeneric = DataBase::getInstance();
        $sql = "DELETE FROM [user] WHERE email = ?";
        $params = [$obj->email];
        $consulta = $dbGeneric->executeSQL($sql, $params);
        
        return $consulta;
    }

    public function readOneUser($obj) {
        $dbConsulta = DataBase::getInstance();
        $sql = "SELECT * FROM [user] WHERE email = ?";
        $params = [$obj->email];
        $consulta = $dbConsulta->executeSQL($sql, $params);
    
        if (is_array($consulta) && count($consulta) > 0) {
            return $consulta;
        } else {
            return false;
        }
    }
}
