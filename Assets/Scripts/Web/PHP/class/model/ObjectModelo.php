<?php

class ObjectModelo implements \CRUDable
{

    public function read($obj) {
        
        $dbConsulta = DataBase::getInstance('consulta');
        $sql = "SELECT * FROM object";
        $consulta = $dbConsulta->executeSQL($sql);

        return $consulta;
    }

    public function create($obj){
        $dbGeneric = DataBase::getInstance('consulta');
        $sql = "INSERT INTO object (name, path) VALUES (?, ?)";
        $params = [
            $obj->name,
            $obj->path
        ];
        $consulta = $dbGeneric->executeSQL($sql, $params);
        return $consulta;        
    }

    public function update($obj) {
        $db = DataBase::getInstance('consulta');
        $sql = "UPDATE object SET name = ?, path = ? WHERE id = ?";
        $params = [
            $obj->name,
            $obj->path,
            $obj->id
        ];
        $consulta = $db->executeSQL($sql, $params);
        return $consulta;
    }

    public function delete($obj) {}

    public function readOneObjectByName($objectName){
        $dbConsulta = DataBase::getInstance('consulta');
        $sql = "SELECT id FROM object WHERE name = ?";
        $params = [$objectName];
        $consulta = $dbConsulta->executeSQL($sql, $params);
    
        return $consulta;
    }
}

