<?php

class GameModelo implements CRUDable
{
    public function read($obj = null) {
        $dbConsulta = DataBase::getInstance();
        $sql = "SELECT * FROM [game]";
        $consulta = $dbConsulta->executeSQL($sql);

        return $consulta;
    }

    public function create($obj) {
        $dbGeneric = DataBase::getInstance();
        $sql = "INSERT INTO [game] (id_user, currentScene, posX, posY, posZ, currentHealth, 
            currentStamina, orderInLayer, sotanoPasado, congeladorPasado, playaPasada, 
            barcoBossPasado, ciudadBossPasado, luzSotanoEncendida, donutDesbloqueado) 
            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
        
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
            $obj->ciudadBossPasado,
            $obj->luzSotanoEncendida,
            $obj->donutDesbloqueado
        ];

        $consulta = $dbGeneric->executeSQL($sql, $params);
        return $consulta;        
    }

    public function update($obj) {
        $dbGeneric = DataBase::getInstance();
        $sql = "UPDATE [game] SET currentScene = ?, posX = ?, posY = ?, posZ = ?, currentHealth = ?, 
            currentStamina = ?, orderInLayer = ?, sotanoPasado = ?, congeladorPasado = ?,
            playaPasada = ?, barcoBossPasado = ?, ciudadBossPasado = ?, luzSotanoEncendida = ?,
            donutDesbloqueado = ? WHERE id_user = ?";

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
            $obj->luzSotanoEncendida,
            $obj->donutDesbloqueado,
            $obj->email_id
        ];

        $consulta = $dbGeneric->executeSQL($sql, $params);
        return $consulta;
    }

    public function delete($obj) {}

    public function readOneGameByUserId($userId) {
        $dbConsulta = DataBase::getInstance();
        $sql = "SELECT * FROM [game] WHERE id_user = ?";
        $params = [$userId];
        $consulta = $dbConsulta->executeSQL($sql, $params);

        if (is_array($consulta) && count($consulta) > 0) {
            return $consulta;
        } else {
            return false;
        }
    }
}
