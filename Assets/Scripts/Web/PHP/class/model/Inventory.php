<?php

class Inventory {

    public $id_game;
    public $slot;
    public $id_object;

    public function __construct($id_game, $slot, $id_object) {
        $this->id_game = $id_game;
        $this->slot = $slot;
        $this->id_object = $id_object;
    }

    public function getIdGame() {
        return $this->id_game;
    }

    public function setIdGame($id_game) {
        $this->id_game = $id_game;
    }

    public function getSlot() {
        return $this->slot;
    }

    public function setSlot($slot) {
        $this->slot = $slot;
    }

    public function getIdObject() {
        return $this->id_object;
    }

    public function setIdObject($id_object) {
        $this->id_object = $id_object;
    }

}