<?php 
$tabelle="equ";
$where_string='player_id="'.$_POST['player_id'].'"';

include ("db.php");
$datenbank->sql_delete($tabelle, $where_string)

?>