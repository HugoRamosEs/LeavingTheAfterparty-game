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
            echo '{"codigo":201,"mensaje":"Direcció de correu no vàlida","respuesta":{}}';
        } else {
            if (strlen($username) == 0) {
                $errors["username"] = "El usuario no puede estar vacío.";
                echo '{"codigo": 202, "mensaje":"L+usuari no pot estar buit", "respuesta":{}}';
            } else {
                if (strlen($password) == 0) {
                    $errors["password"] = "La contraseña no puede estar vacía.";
                    echo '{"codigo": 203, "mensaje":"La contrasenya no pot estar buida", "respuesta":{}}';
                } else {
                    $this->user = new User('', '', '');
                    $this->user->setEmail($email);
                    $this->user->setUsuario($username);
                    $this->user->setContrasenya($hashPassword);

                    if (empty($errors)) {
                        $uModel = new UserModelo();
                        $existeUser = $uModel->readOneUser($this->user);

                        if (count($existeUser) < 1) {

                            if ($uModel->create($this->user)) {

                                $usuarioCreado = $uModel->readOneUser($this->user);

                                $usuario = $usuarioCreado[0];
                                $emailDevuelto = $usuario[0];
                                $usuarioDevuelto = $usuario[1];

                                $texto = "{\"id\":\"" . $emailDevuelto . "\" ,\"username\":\"" . $usuarioDevuelto . "\"}";

                                echo '{"codigo": 206, "mensaje":"Feliciats! S+ha registrat a Leaving the After Party. En breus, se l+hi redirigirà al joc.", "respuesta":' . $texto . '}';
                            } else {
                                echo '{"codigo": 403, "mensaje":"No s+ha pogut crear l+usuari. Siusplau, avisi a l+administrador.", "respuesta":{}}';
                            }
                        } else {
                            echo '{"codigo": 205, "mensaje":"El correu ja existeix. Prova amb un altre.", "respuesta":{}}';
                        }
                    } else {
                        echo '{"codigo": 204, "mensaje":"Hi ha errors en el formulari de registre.", "respuesta":{}}';
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

                $errors["password"] = "La contrasenya no pot estar buida.";
                echo '{"codigo": 203, "mensaje":"La contrasenya no pot estar buida.", "respuesta":{}}';
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
                            echo '{"codigo": 209, "mensaje":"S+ha iniciat de sessió correctament.", "respuesta":' . $texto . '}';
                        } else {
                            echo '{"codigo": 208, "mensaje":"El correu o la contrasenya són incorrectes.", "respuesta":{}}';
                        }
                    } else {
                        echo '{"codigo": 207, "mensaje":"El correu no està registrat.", "respuesta":{}}';
                    }
                } else {
                    echo '{"codigo": 204, "mensaje":"Hi ha errors en el formulari de registre.", "respuesta":{}}';
                }
            }
        } else {
            $errors["email"] = "Direcció de correu no vàlida.";
            echo '{"codigo": 201, "mensaje":"Direcció de correu no vàlida", "respuesta":{}}';
        }
    }

    public function guardarPartida($datos)
    {
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
            $currentHp = Controller::sanitize($datos['currentHp']);
            $currentStamina = Controller::sanitize($datos['currentStamina']);

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
            $game = new Game('', $existeUser[0][0], $escena, $posX, $posY, $posZ, $currentHp, $currentStamina, '');
            $existingGame = $gModel->readOneGameByUserId($existeUser[0][0]);

            if (count($existingGame) == 1) {
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
                    $objectId = $oModel->readOneObjectByName($objectName)[0][0];
                    $inventory = new Inventory($existingGame[0][0], $slot, $objectId);
                    $iModel->create($inventory);
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

        if (count($existeUser) == 1) {

            $gModel = new GameModelo();
            $game = $gModel->readOneGameByUserId($existeUser[0][0]);

            if (count($game) == 1) {
                
                $iModel = new InventoryModelo();
                $inventory = $iModel->readOneInventoryByGameId($game[0][0]);
                
                // var_dump($game);
                // var_dump($inventory);

                $respuestaGame = "{\"id\":\"" . $game[0][0] . "\",\"escena\":\"" . $game[0][2] . "\",\"posX\":\"" . $game[0][3] . "\",\"posY\":\"" . $game[0][4] . "\",\"posZ\":\"" . $game[0][5] . "\",\"currentHp\":\"" . $game[0][6] . "\",\"currentStamina\":\"" . $game[0][7] . "\"}";
                echo '{"codigo": 212, "mensaje":"Cargado correctamente.", "respuesta":{"game":' . $respuestaGame . ', "inventory":' . json_encode($inventory) . '}}';
            } else {
                echo '{"codigo": 214, "mensaje":"No se ha encontrado una partida para cargar. Por favor, cree una nueva.", "respuesta":{}}';
            }

        } else {
            echo '{"codigo": 207, "mensaje":"El correo no esta registrado.", "respuesta":{}}';
        }

    }
}
