<?php

class UserController extends Controller {
    
    public $user;
    
    public function __construct() {
        $this->user = (is_null($param)) ? new User('','','','','','','','','','','','') : $param;
    }

    public function register(User $user = null) {

        // if ($_SERVER["REQUEST_METHOD"]=="POST" && (isset($_POST["submit"]))) {
            $email = Controller::sanitize($user->getEmail());
            $username = Controller::sanitize($user->getUsuario());
            $password = Controller::sanitize($user->getContrasenya());

            $hashPassword = password_hash($password, PASSWORD_DEFAULT);

            // $email = Controller::sanitize($_GET['email']);
            // $username = Controller::sanitize($_GET['username']);
            // $password = Controller::sanitize($_GET['password']);
    
            if(!filter_var($email, FILTER_VALIDATE_EMAIL)) {
                $errors["email"] = "Dirección de correo no válida.";
                echo '{"codigo": 201, "mensaje":"Dirección de correo no válida", "respuesta":""}'; 
            }
            if (strlen($username) == 0) {
                $errors["username"] = "El usuario no puede estar vacío.";
                echo '{"codigo": 202, "mensaje":"El usuario no puede estar vacío", "respuesta":""}'; 
            }
            if (strlen($password) == 0) {
                $errors["password"] = "La contraseña no puede estar vacía.";
                echo '{"codigo": 203 "mensaje":"La contraseña no puede estar vacía", "respuesta":""}'; 
            }

            $this->user = new User('','','');

            $this->user->setEmail($email);
            $this->user->setUsuario($username);
            $this->user->setContrasenya($hashPassword);

            if (empty($errors)) { 
                $uModel = new UserModelo();
                $existeUser = $uModel->readOneUser($this->user);

                if (count($existeUser) < 1) {
                    
                    $createUser = new UserModelo();
                    if($createUser->create($this->user)) {
                        echo '{"codigo": 206, "mensaje":"Usuario creado con éxito", "respuesta":""}';
                        $consulta = $createUser->readOneUser($this->user);
                        return $consulta;
                    } else {
                        echo '{"codigo": 403, "mensaje":"No se ha podido crear el usuario. Por favor, avisa al administrador", "respuesta":""}';
                    }

                } else {
                    echo '{"codigo": 205, "mensaje":"El correo ya existe", "respuesta":""}';
                }
            } else {
                echo '{"codigo": 204, "mensaje":"Hay errores en el formulario de registro", "respuesta":""}'; 
            }
    
        // } else {
        //     echo '{"codigo": 402, "mensaje":"La petición no ha sido enviada por POST", "respuesta":""}';
        // }

    }

    public function login(User $user = null) {

        // if ($_SERVER["REQUEST_METHOD"]=="POST" && (isset($_POST["submit"]))) {

            $email = Controller::sanitize($user->getEmail());
            $password = Controller::sanitize($user->getContrasenya());

            // $email = Controller::sanitize($_GET['email']);
            // $password = Controller::sanitize($_GET['password']);
    
            if(!filter_var($email, FILTER_VALIDATE_EMAIL)) {
                $errors["email"] = "Dirección de correo no válida.";
                echo '{"codigo": 201, "mensaje":"Dirección de correo no válida", "respuesta":""}'; 
            }
            if (strlen($password) == 0) {
                $errors["password"] = "La contraseña no puede estar vacía.";
                echo '{"codigo": 203 "mensaje":"La contraseña no puede estar vacía", "respuesta":""}'; 
            }

            $this->user = new User('','','');

            $this->user->setEmail($email);
            $this->user->setContrasenya($password);

            if (empty($errors)) { 
                $uModel = new UserModelo();
                $existeUser = $uModel->readOneUser($this->user);

                if (count($existeUser) == 1) {
                    $usuario = $existeUser[0];
                    $contrasenyaEntrada = $usuario[2];
                    $emailEntrado = $usuario[0];

                    if ($email === $emailEntrado && password_verify($password, $contrasenyaEntrada)) {
                        echo '{"codigo": 209, "mensaje":"Se ha iniciado de sesión correctamente", "respuesta":""}';
                    } else {
                        echo '{"codigo": 208, "mensaje":"El correo o la contraseña son incorrectos", "respuesta":""}';
                    }

                } else {
                    echo '{"codigo": 207, "mensaje":"El correo no esta registrado", "respuesta":""}';
                }
            } else {
                echo '{"codigo": 204, "mensaje":"Hay errores en el formulario de registro", "respuesta":""}'; 
            }

        // } else {
        //     echo '{"codigo": 402, "mensaje":"La petición no ha sido enviada por POST", "respuesta":""}';
        // }
    }

}


// include "footer.php";