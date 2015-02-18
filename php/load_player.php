<?php
    // Alle Vars die Übergeben werden
    $user_id = $_POST['id'];

    // Alle Incs
    include_once("db.php"); // damit das auf der Page klappt

    $player_data = $datenbank->sql_select("player", "*", "player_id='" . $user_id . "'");
    foreach ($player_data as $key => $value) {
		echo $value['data'];
    }
?>