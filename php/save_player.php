<?php

    // Alle Incs
    include_once("db.php"); // damit das auf der Page klappt

    //player_save
    $new_werte = $_POST;
    $datenbank->sql_insert_update("player", $new_werte);
    unset($new_werte);
    $user_data = $datenbank->sql_select("player", "*", "player_id='" . $_POST['player_id'] . "'");

    foreach ($user_data as $key => $value) {
	echo $value.";"; // brauch man eigentlich gar nicht, weil mit der Ausgabe nichts gemacht wird!
    }
?>