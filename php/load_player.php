<?php
    // Alle Vars die Übergeben werden
    $user_name = $_POST['user_name'];
    $user_password = sha1(md5($_POST['user_password']));

    // Alle Incs
    include_once("db.php"); // damit das auf der Page klappt

  $user_data = $datenbank->sql_select("user", "*", "user_name='" . $user_name . "'");
 if ($user_password == $user_data[0]['user_password']) {
    //player_save
    $player_data = $datenbank->sql_select("player", "*", "player_id='" . $user_data[0]['id'] . "'");

    foreach ($player_data as $key => $value) {
	echo $user_data[0]['user_name'].";";
	echo $value['hp'].";";
	echo $value['maxhp'].";";
	echo $value['mana'].";";
	echo $value['maxmana'].";";
	echo $value['xp'].";";
	echo $value['lvl'].";";
	echo $value['gold'].";";
	echo $value['pwr'].";";
	echo $value['armor'].";";
	echo $value['agility'].";";
	echo $value['posx'].";";
	echo $value['posy'].";";
	echo $value['lastposx'].";";
	echo $value['lastposy'].";";
	echo $user_data[0]['id'].";";
    }
} else {
	echo "-1";
}


?>