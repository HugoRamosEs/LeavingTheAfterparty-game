<?php

class UserModelo implements \CRUDable
{

    public function read($obj) {
        
        $dbConsulta = DataBase::getInstance('consulta');
        $sql = "SELECT * FROM user";
        $consulta = $dbConsulta->executeSQL($sql);

        return $consulta;
    }

    public function create($obj){
        $dbGeneric = DataBase::getInstance('consulta');
        $sql = "INSERT INTO user (email, username, password) VALUES (?, ?, ?)";
        $params = [$obj->email, $obj->usuario, $obj->contrasenya];
        $consulta = $dbGeneric->executeSQL($sql, $params);
        return $consulta;        
    }

    public function update($obj) {
        $db = new mysqli('localhost', 'usr_generic', '2024@Thos', 'myweb');
        
        if($db->connect_error){
            die("La connexió ha fallat, error número " .
                $db->connect_errno . ": " . $db->connect_error);
        }
        
        $stmt = $db->prepare("UPDATE user SET status = ? WHERE id = ?");
        $stmt->bind_param("is", $status, $id);
        
        $id = $obj;
        $status = 1;
        
        $stmt->execute();
        
        $stmt->close();
        $db->close();
        
    }

    public function delete($obj)
    {}

    public function readOneUser($obj) {
        
        $dbConsulta = DataBase::getInstance('consulta');
        $sql = "SELECT * FROM user WHERE email = ?";
        $params = [$obj->email];
        $consulta = $dbConsulta->executeSQL($sql, $params);

        return $consulta;
    }
}

