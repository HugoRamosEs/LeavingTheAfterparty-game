<?php

class GameModelo implements \CRUDable
{

    public function read($obj) {
        
        $dbConsulta = DataBase::getInstance('consulta');
        $sql = "SELECT * FROM game";
        $consulta = $dbConsulta->executeSQL($sql);

        return $consulta;
    }

    public function create($obj){
        $dbGeneric = DataBase::getInstance('consulta');
        $sql = "INSERT INTO game (id_user, currentScene, posX, posY, posZ, currentHealth, 
        currentStamina, orderInLayer, sotanoPasado, congeladorPasado, playaPasada, barcoBossPasado,
        ciudadBossPasado) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
        $params = [
            $obj->email_id,
            $obj->scene,
            $obj->posX,
            $obj->posY,
            $obj->posZ,
            $obj->currentHp,
            $obj->currentStamina,
            $obj->orderInLayer,
            $obj->sotanoPasado,
            $obj->congeladorPasado,
            $obj->playaPasada,
            $obj->barcoBossPasado,
            $obj->ciudadBossPasado
        ];
        $consulta = $dbGeneric->executeSQL($sql, $params);
        return $consulta;        
    }

    public function update($obj) {
        $db = DataBase::getInstance('consulta');
        $sql = "UPDATE game SET currentScene = ?, posX = ?, posY = ?, posZ = ?, currentHealth = ?, 
        currentStamina = ?, orderInLayer = ?, sotanoPasado = ?, congeladorPasado = ?,
        playaPasada = ?, barcoBossPasado = ?, ciudadBossPasado = ? WHERE id_user = ?";
        $params = [
            $obj->scene,
            $obj->posX,
            $obj->posY,
            $obj->posZ,
            $obj->currentHp,
            $obj->currentStamina,
            $obj->orderInLayer,
            $obj->sotanoPasado,
            $obj->congeladorPasado,
            $obj->playaPasada,
            $obj->barcoBossPasado,
            $obj->ciudadBossPasado,
            $obj->email_id
        ];
        $consulta = $db->executeSQL($sql, $params);
        return $consulta;
    }

    public function delete($obj) {}

    public function readOneGameByUserId($userId){
        $dbConsulta = DataBase::getInstance('consulta');
        $sql = "SELECT * FROM game WHERE id_user = ?";
        $params = [$userId];
        $consulta = $dbConsulta->executeSQL($sql, $params);
    
        return $consulta;
    }
}

