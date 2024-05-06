<?php

class InventoryModelo implements \CRUDable
{

    public function read($obj)
    {

        $dbConsulta = DataBase::getInstance('consulta');
        $sql = "SELECT * FROM inventory";
        $consulta = $dbConsulta->executeSQL($sql);

        return $consulta;
    }

    public function create($obj)
    {
        $dbGeneric = DataBase::getInstance('consulta');
        $sql = "INSERT INTO inventory (id_game, slot, id_object) VALUES (?, ?, ?)";
        $params = [
            $obj->id_game,
            $obj->slot,
            $obj->id_object
        ];
        $consulta = $dbGeneric->executeSQL($sql, $params);
        return $consulta;
    }

    public function update($obj)
    {
        $db = DataBase::getInstance('consulta');
        $sql = "UPDATE inventory SET slot = ?, id_object = ? WHERE id_game = ?";
        $params = [
            $obj->slot,
            $obj->id_object,
            $obj->id_game
        ];
        $consulta = $db->executeSQL($sql, $params);
        return $consulta;
    }

    public function delete($obj)
    {
    }

    public function readOneInventoryByGameId($gameId)
    {
        $dbConsulta = DataBase::getInstance('consulta');
        $sql = "SELECT * FROM inventory WHERE id_game = ?";
        $params = [$gameId];
        $consulta = $dbConsulta->executeSQL($sql, $params);

        return $consulta;
    }

    public function deleteAllByGameId($gameId)
    {
        $dbConsulta = DataBase::getInstance('consulta');
        $sql = "DELETE FROM inventory WHERE id_game = ?";
        $params = [$gameId];
        $dbConsulta->executeSQL($sql, $params);
    }
}
