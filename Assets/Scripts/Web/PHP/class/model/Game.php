<?php

class Game {

    public $id;
    public $email_id;
    public $scene;
    public $posX;
    public $posY;
    public $posZ;
    public $currentHp;
    public $currentStamina;
    public $inventory;
    public $orderInLayer;
    public $sotanoPasado;
    public $congeladorPasado;
    public $playaPasada;
    public $barcoBossPasado;
    public $ciudadBossPasado;
    public $luzSotanoEncendida;
    public $donutDesbloqueado;

    public function __construct($id, $email_id, $scene, $posX, $posY, $posZ, $currentHp, $currentStamina, $inventory, $orderInLayer, $sotanoPasado, $congeladorPasado, $playaPasada, $barcoBossPasado, $ciudadBossPasado, $luzSotanoEncendida, $donutDesbloqueado) {
        $this->id = $id;
        $this->email_id = $email_id;
        $this->scene = $scene;
        $this->posX = $posX;
        $this->posY = $posY;
        $this->posZ = $posZ;
        $this->currentHp = $currentHp;
        $this->currentStamina = $currentStamina;
        $this->inventory = $inventory;
        $this->orderInLayer = $orderInLayer;
        $this->sotanoPasado = $sotanoPasado;
        $this->congeladorPasado = $congeladorPasado;
        $this->playaPasada = $playaPasada;
        $this->barcoBossPasado = $barcoBossPasado;
        $this->ciudadBossPasado = $ciudadBossPasado;
        $this->luzSotanoEncendida = $luzSotanoEncendida;
        $this->donutDesbloqueado = $donutDesbloqueado;
    }

    public function getId() {
        return $this->id;
    }

    public function getEmailId() {
        return $this->email_id;
    }

    public function getScene() {
        return $this->scene;
    }

    public function getPosX() {
        return $this->posX;
    }

    public function getPosY() {
        return $this->posY;
    }

    public function getPosZ() {
        return $this->posZ;
    }

    public function getCurrentHp() {
        return $this->currentHp;
    }

    public function getCurrentStamina() {
        return $this->currentStamina;
    }

    public function getOrderInLayer() {
        return $this->orderInLayer;
    }

    public function getSotanoPasado() {
        return $this->sotanoPasado;
    }

    public function getCongeladorPasado() {
        return $this->congeladorPasado;
    }

    public function getPlayaPasada() {
        return $this->playaPasada;
    }

    public function getBarcoBossPasado() {
        return $this->barcoBossPasado;
    }

    public function getCiudadBossPasado() {
        return $this->ciudadBossPasado;
    }

    public function getLuzSotanoEncendida() {
        return $this->luzSotanoEncendida;
    }

    public function getDonutDesbloqueado() {
        return $this->donutDesbloqueado;
    }

    public function setId($id) {
        $this->id = $id;
    }

    public function setEmailId($email_id) {
        $this->email_id = $email_id;
    }

    public function setScene($scene) {
        $this->scene = $scene;
    }

    public function setPosX($posX) {
        $this->posX = $posX;
    }

    public function setPosY($posY) {
        $this->posY = $posY;
    }

    public function setPosZ($posZ) {
        $this->posZ = $posZ;
    }

    public function setCurrentHp($currentHp) {
        $this->currentHp = $currentHp;
    }

    public function setCurrentStamina($currentStamina) {
        $this->currentStamina = $currentStamina;
    }

    public function getInventory() {
        return $this->inventory;
    }

    public function setInventory($inventory) {
        $this->inventory = $inventory;
    }

    public function setOrderInLayer($orderInLayer) {
        $this->orderInLayer = $orderInLayer;
    }

    public function setSotanoPasado($sotanoPasado) {
        $this->sotanoPasado = $sotanoPasado;
    }

    public function setCongeladorPasado($congeladorPasado) {
        $this->congeladorPasado = $congeladorPasado;
    }

    public function setPlayaPasada($playaPasada) {
        $this->playaPasada = $playaPasada;
    }

    public function setBarcoBossPasado($barcoBossPasado) {
        $this->barcoBossPasado = $barcoBossPasado;
    }

    public function setCiudadBossPasado($ciudadBossPasado) {
        $this->ciudadBossPasado = $ciudadBossPasado;
    }

    public function setLuzSotanoEncendida($luzSotanoEncendida) {
        $this->luzSotanoEncendida = $luzSotanoEncendida;
    }

    public function setDonutDesbloqueado($donutDesbloqueado) {
        $this->donutDesbloqueado = $donutDesbloqueado;
    }
}