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

    public function __construct($id, $email_id, $scene, $posX, $posY, $posZ, $currentHp, $currentStamina, $inventory) {
        $this->id = $id;
        $this->email_id = $email_id;
        $this->scene = $scene;
        $this->posX = $posX;
        $this->posY = $posY;
        $this->posZ = $posZ;
        $this->currentHp = $currentHp;
        $this->currentStamina = $currentStamina;
        $this->inventory = $inventory;
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
}