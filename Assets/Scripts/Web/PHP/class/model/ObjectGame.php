<?php

class ObjectGame {

    public $id;
    public $name;
    public $path;

    public function __construct($id, $name, $path) {
        $this->id = $id;
        $this->name = $name;
        $this->path = $path;
    }

    public function getId() {
        return $this->id;
    }

    public function getName() {
        return $this->name;
    }

    public function getPath() {
        return $this->path;
    }

    public function setId($id) {
        $this->id = $id;
    }

    public function setName($name) {
        $this->name = $name;
    }

    public function setPath($path) {
        $this->path = $path;
    }
    
}