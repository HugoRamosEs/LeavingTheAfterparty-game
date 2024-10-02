<?php

class UserController extends Controller
{

    public $user;

    public function register(User $user = null)
    {
        $email = Controller::sanitize($user->getEmail());
        $username = Controller::sanitize($user->getUsuario());
        $password = Controller::sanitize($user->getContrasenya());

        $hashPassword = password_hash($password, PASSWORD_DEFAULT);

        if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
            $errors["email"] = "Dirección de correo no válida.";
            echo '{"codigo":201,"mensaje":"Dirección de correu no válida","respuesta":{}}';
        } else {
            if (strlen($username) == 0) {
                $errors["username"] = "El usuario no puede estar vacío.";
                echo '{"codigo": 202, "mensaje":"El usuario no puede estar vacío.", "respuesta":{}}';
            } else {
                if (strlen($password) == 0) {
                    $errors["password"] = "La contraseña no puede estar vacía.";
                    echo '{"codigo": 203, "mensaje":"La contraseña no puede estar vacía.", "respuesta":{}}';
                } else {
                    $this->user = new User('', '', '');
                    $this->user->setEmail($email);
                    $this->user->setUsuario($username);
                    $this->user->setContrasenya($hashPassword);

                    if (empty($errors)) {
                        $uModel = new UserModelo();
                        $existeUser = $uModel->readOneUser($this->user);

                        if ($existeUser == false) {
                            if ($uModel->create($this->user)) {

                                $usuarioCreado = $uModel->readOneUser($this->user);

                                $usuario = $usuarioCreado[0];
                                $emailDevuelto = $usuario[0];
                                $usuarioDevuelto = $usuario[1];

                                $texto = "{\"id\":\"" . $emailDevuelto . "\" ,\"username\":\"" . $usuarioDevuelto . "\"}";

                                echo '{"codigo": 206, "mensaje":"¡Felicidades! Se ha registrado en Leaving the After Party. En breves, se le redirigirá al juego.", "respuesta":' . $texto . '}';
                            } else {
                                echo '{"codigo": 403, "mensaje":"No se pudo crear el usuario. Por favor, avise al administrador..", "respuesta":{}}';
                            }
                        } else {
                            echo '{"codigo": 205, "mensaje":"El correo ya existe. Por favor, use otro.", "respuesta":{}}';
                        }
                    } else {
                        echo '{"codigo": 204, "mensaje":"Hay errores en el formulario de registro.", "respuesta":{}}';
                    }
                }
            }
        }
    }

    public function login(User $user = null)
    {
        $email = Controller::sanitize($user->getEmail());
        $password = Controller::sanitize($user->getContrasenya());

        if (filter_var($email, FILTER_VALIDATE_EMAIL)) {

            if (strlen($password) == 0) {

                $errors["password"] = "La contraseña no puede estar vacia.";
                echo '{"codigo": 203, "mensaje":"La contraseña no puede estar vacía.", "respuesta":{}}';
            } else {
                $this->user = new User('', '', '');

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

                            $texto = "{\"id\":\"" . $emailDevuelto . "\" ,\"username\":\"" . $usuarioDevuelto . "\"}";
                            echo '{"codigo": 209, "mensaje":"Se ha iniciado sesión correctamente.", "respuesta":' . $texto . '}';
                        } else {
                            echo '{"codigo": 208, "mensaje":"El correo o la contraseña son incorrectos.", "respuesta":{}}';
                        }
                    } else {
                        echo '{"codigo": 207, "mensaje":"El correo no está registrado.", "respuesta":{}}';
                    }
                } else {
                    echo '{"codigo": 204, "mensaje":"Hay errores en el formulario de registro.", "respuesta":{}}';
                }
            }
        } else {
            $errors["email"] = "Dirección de correo no válida.";
            echo '{"codigo": 201, "mensaje":"Dirección de correo no válida.", "respuesta":{}}';
        }
    }

    public function guardarPartida($datos)
    {
        // var_dump($datos);

        $email = Controller::sanitize($datos['email']);

        $this->user = new User('', '', '');
        $this->user->setEmail($email);

        $uModel = new UserModelo();
        $existeUser = $uModel->readOneUser($this->user);

        if (count($existeUser) == 1) {
            $escena = Controller::sanitize($datos['escena']);
            $posX = Controller::sanitize($datos['posX']);
            $posY = Controller::sanitize($datos['posY']);
            $posZ = Controller::sanitize($datos['posZ']);
            $currentHp = floatval(Controller::sanitize($datos['currentHp']));
            $currentStamina = floatval(Controller::sanitize($datos['currentStamina']));
            $orderInLayer = Controller::sanitize($datos['orderInLayer']);
            $sotanoPasado = Controller::sanitize($datos['sotanoPasado']);
            $congeladorPasado = Controller::sanitize($datos['congeladorPasado']);
            $playaPasada = Controller::sanitize($datos['playaPasada']);
            $barcoBossPasado = Controller::sanitize($datos['barcoBossPasado']);
            $ciudadBossPasado = Controller::sanitize($datos['ciudadBossPasado']);
            $luzSotanoEncendida = Controller::sanitize($datos['luzSotanoEncendida']);
            $donutDesbloqueado = Controller::sanitize($datos['donutDesbloqueado']);

            $inventoryItems = Controller::sanitize($datos['inventory']);
            $inventoryItems = explode(", ", $inventoryItems);

            $parsedInventoryItems = [];
            foreach ($inventoryItems as $item) {
                $item = str_replace(array('(', ')'), '', $item);
                $itemParts = explode(',', $item);
                if (count($itemParts) == 2) {
                    $parsedInventoryItems[] = ['Slot' => $itemParts[0], 'ItemName' => $itemParts[1]];
                }
            }

            $gModel = new GameModelo();
            $game = new Game('', $existeUser[0][0], $escena, $posX, $posY, $posZ, $currentHp, $currentStamina, '', $orderInLayer, $sotanoPasado, $congeladorPasado, $playaPasada, $barcoBossPasado, $ciudadBossPasado, $luzSotanoEncendida, $donutDesbloqueado);
            $existingGame = $gModel->readOneGameByUserId($existeUser[0][0]);

            if ($existingGame && is_array($existingGame) && count($existingGame) === 1) {
                $gModel->update($game);
            } else {
                $gModel->create($game);
                $existingGame = $gModel->readOneGameByUserId($existeUser[0][0]);
            }

            $iModel = new InventoryModelo();
            $iModel->deleteAllByGameId($existingGame[0][0]);

            $oModel = new ObjectModelo();
            foreach ($parsedInventoryItems as $item) {
                $slot = $item['Slot'];
                $objectName = $item['ItemName'];

                if ($objectName != '') {
                    $objectData = $oModel->readOneObjectByName($objectName);

                    if ($objectData !== false) {
                        $objectId = $objectData[0][0]; 
                        $inventory = new Inventory($existingGame[0][0], $slot, $objectId);
                        $iModel->create($inventory);
                    }
                }
            }

            echo '{"codigo": 210, "mensaje":"Partida guardada correctamente.", "respuesta":{}}';
        } else {
            echo '{"codigo": 207, "mensaje":"El correo no esta registrado.", "respuesta":{}}';
        }
    }

    public function cargarPartida($datos) {
        $email = Controller::sanitize($datos['email']);

        $this->user = new User('', '', '');
        $this->user->setEmail($email);

        $uModel = new UserModelo();
        $existeUser = $uModel->readOneUser($this->user);

        if ($existeUser && is_array($existeUser) && count($existeUser) == 1) {
            $gModel = new GameModelo();
            $game = $gModel->readOneGameByUserId($existeUser[0][0]);

            if ($game && is_array($game) && count($game) === 1) {
                $iModel = new InventoryModelo();
                $inventory = $iModel->readOneInventoryByGameId($game[0][0]);
                
                // var_dump($game);
                // var_dump($inventory);

                $respuestaGame = "&&id:" . $game[0][0] .
                    "&&escena:" . $game[0][2] .
                    "&&posX:" . str_replace(',', '.', $game[0][3]) .
                    "&&posY:" . str_replace(',', '.', $game[0][4]) .
                    "&&posZ:" . str_replace(',', '.', $game[0][5]) .
                    "&&currentHp:" . str_replace(',', '.', $game[0][6]) .
                    "&&currentStamina:" . str_replace(',', '.', $game[0][7]) . 
                    "&&orderInLayer:" . $game[0][8] .
                    "&&sotanoPasado:" . $game[0][9] .
                    "&&congeladorPasado:" . $game[0][10] .
                    "&&playaPasada:" . $game[0][11] .
                    "&&barcoBossPasado:" . $game[0][12] .
                    "&&ciudadBossPasado:" . $game[0][13] . 
                    "&&luzSotanoEncendida:" . $game[0][14] .
                    "&&donutDesbloqueado:" . $game[0][15];

                $inventoryString = json_encode($inventory);
                $formatted_respuesta = '{"codigo": 212, "mensaje":"Cargado correctamente.", "respuesta":"' . $respuestaGame . '","inventario":' . $inventoryString . '}';
                echo $formatted_respuesta;

            } else {
                echo '{"codigo": 214, "mensaje":"No se ha encontrado una partida para cargar. Por favor, cree una nueva.", "respuesta":{}}';
            }

        } else {
            echo '{"codigo": 207, "mensaje":"El correo no esta registrado.", "respuesta":{}}';
        }

    }
}
