<?php

    // Alle Vars die Übergeben werden
    $user_name = strtolower($_POST['user_name']);
    $user_password = sha1(md5($_POST['user_password']));
    // Alle Incs
    include_once("db.php"); // damit das auf der Page klappt

    //Login Check
    $user_data = $datenbank->sql_select("user", "*", "user_name like '" . $user_name . "'");
    $new_werte['user_name'] = $user_name;
    if (!isset($user_data[0]['id'])) {
		$new_werte['user_password'] = $user_password;
		$datenbank->sql_insert_update("user", $new_werte);
		$user_data = $datenbank->sql_select("user", "*", "user_name='" . $user_name . "'");
    }
    unset($new_werte);
	
	$player_data = $datenbank->sql_select("player", "*", "player_id='" . $user_data[0]['id'] . "'");	
	if (!isset($player_data[0]['data'])) {
		$status="false";
	} else {
		$status="true";
	}
    if ($user_password == $user_data[0]['user_password']) {
	echo $user_data[0]['id'] . ";".$status.";";
    } else {
	echo "-1";
    }
?>