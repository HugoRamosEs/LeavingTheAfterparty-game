<?php

class UserController extends Controller {
    
    public $user;
    
    // public function __construct() {
    //     $this->user = (is_null($param)) ? new User('','','') : $param;
    // }

    public function register(User $user = null) {

        // if ($_SERVER["REQUEST_METHOD"] == "POST") {
            $email = Controller::sanitize($user->getEmail());
            $username = Controller::sanitize($user->getUsuario());
            $password = Controller::sanitize($user->getContrasenya());

            $hashPassword = password_hash($password, PASSWORD_DEFAULT);
    
            if(!filter_var($email, FILTER_VALIDATE_EMAIL)) {
                $errors["email"] = "Dirección de correo no válida.";
                echo '{"codigo":201,"mensaje":"Direcció de correu no vàlida","respuesta":""}'; 
            } else {
                if (strlen($username) == 0) {
                    $errors["username"] = "El usuario no puede estar vacío.";
                    echo '{"codigo": 202, "mensaje":"L+usuari no pot estar buit", "respuesta":""}'; 
                } else {
                    if (strlen($password) == 0) {
                        $errors["password"] = "La contraseña no puede estar vacía.";
                        echo '{"codigo": 203, "mensaje":"La contrasenya no pot estar buida", "respuesta":""}'; 
                    } else {
                        $this->user = new User('','','');
                        $this->user->setEmail($email);
                        $this->user->setUsuario($username);
                        $this->user->setContrasenya($hashPassword);
            
                        if (empty($errors)) { 
                            $uModel = new UserModelo();
                            $existeUser = $uModel->readOneUser($this->user);
            
                            if (count($existeUser) < 1) {
                                
                                if($uModel->create($this->user)) {
                                    
                                    $usuarioCreado = $uModel->readOneUser($this->user);
            
                                    $usuario = $usuarioCreado[0];
                                    $emailDevuelto = $usuario[0];
                                    $usuarioDevuelto =$usuario[1];
            
                                    $texto = "{
                                        #email#:#". $emailDevuelto ."#,
                                        #usuario#:#". $usuarioDevuelto ."#
                                    }";
            
                                    echo '{"codigo": 206, "mensaje":"Feliciats! S+ha registrat a Leaving the After Party. En breus, se l+hi redirigirà al joc.", "respuesta":'. $texto .'}';
                                } else {
                                    echo '{"codigo": 403, "mensaje":"No s+ha pogut crear l+usuari. Siusplau, avisi a l+administrador.", "respuesta":""}';
                                }
            
                            } else {
                                echo '{"codigo": 205, "mensaje":"El correu ja existeix. Prova amb un altre.", "respuesta":""}';
                            }
                        } else {
                            echo '{"codigo": 204, "mensaje":"Hi ha errors en el formulari de registre.", "respuesta":""}'; 
                        }

                    }
                }
            }
            
    
        // } else {
        //     echo '{"codigo": 402, "mensaje":"La petició no ha sigut enviada per POST", "respuesta":""}';
        // }

    }

    public function login(User $user = null) {

        // if ($_SERVER["REQUEST_METHOD"]=="POST" && (isset($_POST["submit"]))) {

            $email = Controller::sanitize($user->getEmail());
            $password = Controller::sanitize($user->getContrasenya());
    
            if (filter_var($email, FILTER_VALIDATE_EMAIL)) {

                if (strlen($password) == 0) {

                    $errors["password"] = "La contrasenya no pot estar buida.";
                    echo '{"codigo": 203, "mensaje":"La contrasenya no pot estar buida.", "respuesta":""}'; 

                } else {
                    
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
                                $usuario = $existeUser[0];
                                $emailDevuelto = $usuario[0];
                                $usuarioDevuelto = $usuario[1];
        
                                $texto = "{
                                    #id#:". $emailDevuelto ."
                                    #username#:". $usuarioDevuelto ."
                                }";
                                echo '{"codigo": 209, "mensaje":"S+ha iniciat de sessió correctament.", "respuesta":""}';
                            } else {
                                echo '{"codigo": 208, "mensaje":"El correu o la contrasenya són incorrectes.", "respuesta":""}';
                            }
        
                        } else {
                            echo '{"codigo": 207, "mensaje":"El correu no està registrat.", "respuesta":""}';
                        }
                    } else {
                        echo '{"codigo": 204, "mensaje":"Hi ha errors en el formulari de registre.", "respuesta":""}'; 
                    }
                }
            } else {
                $errors["email"] = "Direcció de correu no vàlida.";
                echo '{"codigo": 201, "mensaje":"Direcció de correu no vàlida", "respuesta":""}';
            }
            

        // } else {
        //     echo '{"codigo": 402, "mensaje":"La petición no ha sido enviada por POST", "respuesta":""}';
        // }
    }

}