<?php

class DataBase {

    private $link;
    private static $_instance;

    private static $host;
    private static $dbName;
    private static $user;
    private static $password;

    private function __construct($host, $dbName, $user, $password) {
        try {
            $this->link = new PDO("sqlsrv:Server=$host;Database=$dbName", $user, $password);
            $this->link->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        } catch (PDOException $e) {
            throw new Exception("No se ha podido conectar a la base de datos.");
        }
    }

    public static function getInstance() {
        if (!(self::$_instance instanceof self)) {
            self::$host = getenv('DB_HOST');
            self::$dbName = getenv('DB_NAME');
            self::$user = getenv('DB_USER');
            self::$password = getenv('DB_PASSWORD');

            self::$_instance = new self(self::$host, self::$dbName, self::$user, self::$password);
        }

        return self::$_instance;
    }

    public function destroy() {
        $this->link = null;
    }

    public function executeSQL($sSQL, $aParam = null) {
        try {
            $stmt = $this->link->prepare($sSQL);
            if (!$stmt) {
                throw new Exception("Error preparando la consulta: " . implode(", ", $this->link->errorInfo()));
            }
    
            if ($stmt->execute($aParam)) {
                if (stripos(trim($sSQL), 'SELECT') === 0) {
                    $rows = $stmt->fetchAll(PDO::FETCH_NUM);
                    return count($rows) > 0 ? $rows : true;
                }

                return true;
            } else {
                throw new Exception("Error ejecutando la consulta con los parámetros: " . implode(", ", $stmt->errorInfo()));
            }
        } catch (PDOException $e) {
            throw new Exception("Error ejecutando la consulta: " . $e->getMessage());
        }
    }

    public function changeUser($newHost, $newDbName, $newUser, $newPassword) {
        $this->destroy();

        self::$_instance = new self($newHost, $newDbName, $newUser, $newPassword);
    }

    private function __clone() {}
}
